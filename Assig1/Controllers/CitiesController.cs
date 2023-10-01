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

        private readonly EnvDataContext _Context;

        public CitiesController(EnvDataContext context)
        {
            _Context = context;
        }


        public IActionResult Index(int countryID, string? searchTerms)
        {
            return View();
        }
    }
}
