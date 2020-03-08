using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using girlup.lahs.club.Models;

namespace girlup.lahs.club.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/team")]
        public IActionResult Team()
        {
            return View();
        }

        [Route("/events")]
        public IActionResult Events()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
