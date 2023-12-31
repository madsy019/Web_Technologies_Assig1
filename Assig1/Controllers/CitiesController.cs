﻿using Microsoft.AspNetCore.Mvc;
using Assig1.Data;
using Assig1.Models;
using Assig1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Buffers.Text;

namespace Assig1.Controllers
{
    public class CitiesController : Controller
    {

        private readonly EnvDataContext _context;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(EnvDataContext context, ILogger<CitiesController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index(int countryID, string? searchTerm = "")
        {

            //Fetch the country and region information based on provided country ID
            var country = _context.Countries.FirstOrDefault(c => c.CountryId == countryID);

            if (country == null)
            {
                return NotFound();
            }

            //Fetch the list of cities for given country
            var cities = _context.Cities
                .Include(c => c.AirQualityData)
                .Where(c => c.CountryId == countryID)
                .OrderBy(c => c.CityName)
                .ToList();


            //If a search term is provided, filter the cities based on the search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                cities = cities.Where(c => c.CityName.StartsWith(searchTerm)).ToList();
            }

            // Initialize cityData as an empty list
            List<CityDataViewModel> cityData = new List<CityDataViewModel>();


            //For each city, fetch the air quality data and determine the earliest and latest year
            cityData = cities.Select(c =>
            {
                int? earliestYear = null;
                int? latestYear = null;
                if (c.AirQualityData.Any()) // Check if there's any AirQualityData for the city
                {
                    earliestYear = c.AirQualityData.Min(aqd => aqd.Year);
                    latestYear = c.AirQualityData.Max(aqd => aqd.Year);
                }
                return new CityDataViewModel
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                    EarliestYear = earliestYear,
                    LatestYear = latestYear,
                    RecordCount = c.AirQualityData.Count
                };
            }).ToList();


            //Populate the CitiesIndexViewModel
            var viewModel = new CitiesIndexViewModel
            {
                CountryName = country.CountryName,
                ImageUrl = country.ImageUrl,
                RegionName = country.Region?.RegionName,
                CitiesInfo = cityData,
                CountryId = country.CountryId,
            };


            return View(viewModel);
        }

        //HTTP GET request for returning a list of city names based on the provided parameters
        [HttpGet]

        //Returns JSON data containing city names for a given country ID.
        public JsonResult GetCityNames(int countryID)
        {
            var cityNames = _context.Cities
                            .Where(c => c.CountryId == countryID)
                            .OrderBy(c => c.CityName)  // Order cities alphabetically
                            .Select(c => c.CityName)
                            .ToList();
                            

            return Json(cityNames);
        }

        //displaying details for a specific city 
        public IActionResult Details(int id, int countryID)
        {

            // Fetch the city, country, and region information
            var city = _context.Cities
                       .Include(c => c.Country)
                       .ThenInclude(country => country.Region)
                       .Include(c => c.AirQualityData)
                       .ThenInclude(aqd => aqd.AirQualityStations)
                       .ThenInclude(aqs => aqs.StationType)
                       .FirstOrDefault(c => c.CityId == id);

            if (city == null)
            {
                return NotFound();  // Return a 404 error if the city is not found
            }

       

            // Fetch the region for the country
            var region = _context.Regions.FirstOrDefault(r => r.RegionId == city.Country.RegionId);

            // Create a ViewModel to pass the data to the view
            var viewModel = new CityDetailsViewModel
            {
                CityName = city.CityName,
                CountryName = city.Country.CountryName,
                RegionName = city.Country.Region?.RegionName, // Use the null conditional operator in case the region is null
                
                AirQualityDataList = city.AirQualityData.Select(aqd => new CityAirQualityInfoViewModel
                {
                    Year = aqd.Year,
                    AnnualMean = aqd.AnnualMean,
                    TemporalCoverage1 = aqd.TemporalCoverage1,
                    AnnualMeanPm10 = aqd.AnnualMeanPm10,
                    AnnualMeanUgm3 = aqd.AnnualMeanUgm3,
                    TemporalCoverage2 = aqd.TemporalCoverage2,
                    AnnualMeanPm25 = aqd.AnnualMeanPm25,
                    Reference = aqd.Reference,
                    Status = aqd.Status,

                    AirQualityStations = aqd.AirQualityStations.Select(aqs => new AirQualityStationViewModel
                    {
                        StationTypeId = aqs.StationTypeId,
                        AqdId = aqs.AqdId,
                        Number = aqs.Number,
                        StationType = aqs.StationType.StationType
                    }).ToList()
                }).ToList()
            };

            return View(viewModel); // Return the view with the populated ViewModel
        }
    }
}
