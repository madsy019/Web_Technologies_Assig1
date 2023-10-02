using Microsoft.AspNetCore.Mvc;
using Assig1.Data;
using Assig1.Models;
using Assig1.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assig1.Controllers
{
    public class CitiesController : Controller
    {

        private readonly EnvDataContext _context;

        public CitiesController(EnvDataContext context)
        {
            _context = context;
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
                .OrderBy(c => c.CountryId)
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
        public IActionResult GetCityNames(string term, int countryID)
        {
            var cityNames = _context.Cities
                            .Where(c => c.CountryId == countryID && c.CityName.StartsWith(term))
                            .Select(c => c.CityName)
                            .ToList();
                            

            return Json(cityNames);
        }
    }
}
