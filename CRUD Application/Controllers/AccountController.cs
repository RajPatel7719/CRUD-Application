using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using CRUD_Application.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiProvider _apiProvider;

        public AccountController(IApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        public ActionResult Login()
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
                    ViewBag.Message = "Invalid User Name And Password";
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

        public ActionResult Action()
        {
            return View();
        }
    }
}
