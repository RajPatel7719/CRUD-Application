using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
