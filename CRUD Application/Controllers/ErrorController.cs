using Microsoft.AspNetCore.Mvc;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CRUD_Application.Controllers
{
    public class ErrorController : Controller
    {
        //[HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]

        public IActionResult Error(string errCode)
        {
            if (errCode == "500" || errCode == "404" || errCode == "403")
            {
                return View($"~/Views/Error/{errCode}.cshtml");
            }

            return View("~/Views/Shared/404.cshtml");
        }
    }
}
