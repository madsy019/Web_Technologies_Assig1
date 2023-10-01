using Microsoft.AspNetCore.Mvc;

namespace Assig1.Controllers
{
    public class CitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
