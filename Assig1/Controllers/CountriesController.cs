using Microsoft.AspNetCore.Mvc;

namespace Assig1.Controllers
{
    public class CountriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
