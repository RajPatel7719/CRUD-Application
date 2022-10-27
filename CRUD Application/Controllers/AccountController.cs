using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using CRUD_Application.Filter;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;

namespace CRUD_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiProvider _apiProvider;

        public AccountController(IApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var user = await _apiProvider.Login(login);
                var status = user.Status;
                if (status != "Success")
                {
                    ViewBag.Message = login.Message;
                    return View();
                }
                ViewBag.UserName = login.UserName;
                Response.Cookies.Append("Token", user.Token.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                //HttpContext.Session.SetString("Token", user.Token.ToString());

                return RedirectToAction("Index", "User");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _apiProvider.Register(register);
                    var status = user.Status;
                    if (status != "Success")
                    {
                        ViewBag.Message = user.Message;
                        return View();
                    }
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }
    }
}
