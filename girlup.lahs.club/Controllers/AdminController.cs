using girlup.lahs.club.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace girlup.lahs.club.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("/admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet("adduser")]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser(string email, string name)
        {
            IdentityResult IR = await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = name,
                MinutesAttended = 0
            }, "girlup");

            if (IR.Succeeded)
            {
                ViewData["Message"] = $"Created new user with email {email} and name {name} using default password 'girlup' successfully.";
                return View();
            }
            else
            {
                string errors = "Error: ";
                foreach(IdentityError e in IR.Errors)
                {
                    errors += e.Description + " ";
                }
                ViewData["Message"] = errors;

                return View();
            }
        }
    }
}