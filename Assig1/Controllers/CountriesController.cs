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


        /*        public IActionResult Index()
                {
                    return View();
                }*/

        //Index Action to display the list of countires
        public async Task<IActionResult> Index()
        {
            var countries = await _context.Countries
                .OrderBy(c => c.CountryName)
                .ToListAsync();

            return View(countries);

        }
    }
}
