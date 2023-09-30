using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assig1.Data;
using Assig1.Models;


namespace Assig1.Controllers
{
    public class CountriesController : Controller
    {
        private readonly EnvDataContext _context;

        //constructor to load the database 
        public CountriesController(EnvDataContext context)
        {
            _context = context;
        }

        /// Index action method to display the list of countries.
        /// If a region ID is provided, it will display countries only from that region.
        /// If no region ID is provided, it will display all countries.
        public async Task<IActionResult> Index(int? id)
        {
            
            if (id.HasValue)
            {
                // Fetch countries associated with the provided region ID
                var countriesByRegion = await _context.Countries
                                                      .Where(c => c.RegionId == id)
                                                      .OrderBy(c => c.CountryName)
                                                      .ToListAsync();
                return View(countriesByRegion);
            }
            else
            {
                // Fetch all countries from the database
                var allCountries = await _context.Countries.OrderBy(c => c.CountryName).ToListAsync();
                return View(allCountries);
            }
        }

        //Details action to displays the name of the country, and where available, the region the country belongs to. 
        public async Task<IActionResult> Details(int countryID)
        {
            var country = await _context.Countries
                                .Include(c => c.Region)
                                .Include(c => c.CountryEmissions)
                                .ThenInclude(ce => ce.ItemElement)
                                .ThenInclude(ie => ie.Item) // Include the Item navigation property from ItemElement
                                .Include(c => c.TemperatureData)
                                .FirstOrDefaultAsync(c => c.CountryId == countryID);

            if (country == null)
            {
                return NotFound();
            }

            return View(country); // Directly pass the country object since it contains all the necessary data
        }
    }
}
