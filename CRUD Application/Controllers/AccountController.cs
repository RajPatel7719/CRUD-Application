using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CRUD_Application.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IApiProvider _apiProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountController(IApiProvider apiProvider, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _apiProvider = apiProvider;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            var username = _httpContextAccessor.HttpContext.Request.Cookies["User"];
            var password = _httpContextAccessor.HttpContext.Request.Cookies["Pass"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return View(new Login());
            }
            else
            {
                Login login = new() { UserName = username, Password = password, RememberMe = true };
                ViewBag.Password = password;

                return View(login);
            }
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
                Response.Cookies.Append("UserName", login.UserName.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                HttpContext.Session.SetString("UserName", login.UserName.ToString());
                if (login.RememberMe)
                {
                    Response.Cookies.Append("User", login.UserName.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(30) });
                    Response.Cookies.Append("Pass", login.Password.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(30) });
                }
                else
                {
                    Response.Cookies.Delete("User");
                    Response.Cookies.Delete("Pass");
                }
                return RedirectToAction("Index", "Profile");
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("UserName");
            return RedirectToAction("Login", "Account");
        }
    }
}
