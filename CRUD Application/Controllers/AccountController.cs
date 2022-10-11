using Microsoft.AspNetCore.Mvc;
using CRUD_Application.Models;

namespace CRUD_Application.Controllers
{
    public class AccountController : Controller
    { 
        // GET: Account
        public IActionResult Login()
        {
              return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Id,Email,Password")] UserLogin userLogin)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
