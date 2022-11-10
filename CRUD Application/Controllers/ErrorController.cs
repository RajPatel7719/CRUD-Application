using Microsoft.AspNetCore.Mvc;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CRUD_Application.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ErrorController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IActionResult Error()
        {
            var isLogedIn = _contextAccessor.HttpContext.Request.Cookies["Token"];
            if (isLogedIn != null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View("~/Views/Shared/404.cshtml");
        }
    }
}
