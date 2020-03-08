using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace girlup.lahs.club.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        [Route("admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}