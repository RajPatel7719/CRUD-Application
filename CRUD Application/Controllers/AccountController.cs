using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Graph;
using Org.BouncyCastle.Utilities;

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
                var userdetail = _apiProvider.GetUserByEmail(login.UserName).Result.Result;
                ViewBag.UserName = login.UserName;
                Response.Cookies.Append("Token", user.Token.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                Response.Cookies.Append("UserName", login.UserName.ToString(), new CookieOptions() { Expires = DateTime.Now.AddHours(12) });
                HttpContext.Session.SetString("UserName", login.UserName.ToString());

                //var image = File(userdetail.ImageData, "image/*");
                var base64 = Convert.ToBase64String(userdetail.ImageData);
                var imgSrc = String.Format("data:image/*;base64,{0}", base64);
                HttpContext.Session.SetString("ProfileImage", imgSrc);

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
                    if (register.ImageFile != null)
                    {
                        if (register.ImageFile.Length > 0)
                        {
                            using (MemoryStream mStream = new())
                            {
                                await register.ImageFile.CopyToAsync(mStream);
                                register.ImageData = mStream.ToArray();
                                register.ProfilePicture = register.ProfilePicture;
                            }
                        }
                    }
                    Register user = new() 
                    { 
                        UserName = register.UserName,
                        Email = register.Email,
                        Password = register.Password,
                        ProfilePicture = register.ProfilePicture,
                        ImageData = register.ImageData,
                        TwoFactorEnabled = register.TwoFactorEnabled
                    };

                    var userdetail = await _apiProvider.Register(user);
                    var status = userdetail.Status;
                    if (status != "Success")
                    {
                        ViewBag.Message = userdetail.Message;
                        return View();
                    }
                    //var image = await _imageUpload.SaveImage(register.ImageFile, register.UserName);
                    //if (!string.IsNullOrEmpty(image))
                    //{
                    //    user.ProfilePicture = image;
                    //    await _apiProvider.EditProfile(user);
                    //}
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
