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


        public IActionResult Index(int countryID, string? searchTerm)
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


            // Populate the CitiesIndexViewModel
            var viewModel = new CitiesIndexViewModel
            {
                CountryName = country.CountryName,
                ImageUrl = country.ImageUrl,
                RegionName = country.Region?.RegionName,
                
            };


            return View(viewModel);
        }
    }
}
