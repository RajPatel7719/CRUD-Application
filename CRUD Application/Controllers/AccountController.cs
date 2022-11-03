using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Mvc;
using CRUD_Application.Models.ModelsDTO;
using AutoMapper;

namespace CRUD_Application.Controllers
{
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
                _httpContextAccessor.HttpContext.Session.SetString("UserName", login.UserName.ToString());

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

        public IActionResult EditProfile(string email)
        {
            var user = _apiProvider.GetUserByEmail(email);
            if (user == null) 
                return RedirectToActionPermanent("Index", "User");

            return View(user.Result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedUser = _mapper.Map<Register>(register);
                    await _apiProvider.EditProfile(mappedUser);

                    return RedirectToActionPermanent("Index", "User");
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
