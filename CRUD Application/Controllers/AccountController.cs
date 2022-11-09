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
        private readonly IMapper _mapper;
        private readonly IApiProvider _apiProvider;
        private readonly IImageUpload _imageUpload;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IApiProvider apiProvider,
                                 IHttpContextAccessor httpContextAccessor,
                                 IMapper mapper,
                                 IImageUpload imageUpload)
        {
            _apiProvider = apiProvider;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _imageUpload = imageUpload;
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
                var userdetail = await _apiProvider.GetUserByEmail(login.UserName);
                ViewBag.UserName = login.UserName;
                Response.Cookies.Append("Token", user.Token.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                Response.Cookies.Append("UserName", login.UserName.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                HttpContext.Session.SetString("UserName", login.UserName.ToString());
                HttpContext.Session.SetString("ProfileImage", userdetail.Result.ProfilePicture.ToString());

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
                    Register user = new() 
                    { 
                        UserName = register.UserName,
                        Email = register.Email,
                        Password = register.Password,
                        ProfilePicture = register.ProfilePicture,
                        TwoFactorEnabled = register.TwoFactorEnabled
                    };

                    var userdetail = await _apiProvider.Register(user);
                    var status = userdetail.Status;
                    if (status != "Success")
                    {
                        ViewBag.Message = userdetail.Message;
                        return View();
                    }
                    var image = await _imageUpload.SaveImage(register.ImageFile, register.UserName);
                    if (!string.IsNullOrEmpty(image))
                    {
                        user.ProfilePicture = image;
                        await _apiProvider.EditProfile(user);
                    }
                    return RedirectToAction("Login", "Account");
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
