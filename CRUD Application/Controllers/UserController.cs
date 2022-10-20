using CRUD_Application.Models;
using CRUD.ServiceProvider;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiProvider _apiProvider;

        public UserController(IApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        public async Task<IActionResult> Index(string sortField, string currentSortField, string currentSortOrder, string currentFilter, string SearchString, int? pageNo)
        {
            //var Auth = HttpContext.Session.GetString("Token");
            var user = await _apiProvider.GetUser();
            var result = user.Result;
            if (SearchString != null)
            {
                pageNo = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewData["CurrentSort"] = sortField;
            ViewBag.CurrentFilter = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                result = result.Where(s => s.FirstName.Contains(SearchString)).ToList();
            }
            result = this.SortUserData(result, sortField, currentSortField, currentSortOrder);
            int pageSize = 10;

            return View(PagingList<User1>.CreateAsync(result.AsQueryable(), pageNo ?? 1, pageSize));
        }

        public List<User1> SortUserData(IEnumerable<User1> user, string sortField, string currentSortField, string currentSortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "Id";
                ViewBag.SortOrder = "Asc";
            }
            else
            {
                if (currentSortField == sortField)
                {
                    ViewBag.SortOrder = currentSortOrder == "Asc" ? "Desc" : "Asc";
                }
                else
                {
                    ViewBag.SortOrder = "Asc";
                }
                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(User1).GetProperty(ViewBag.SortField);
            if (ViewBag.SortOrder == "Asc")
            {
                return user.OrderBy(s => propertyInfo.GetValue(s, null)).ToList();
            }
            else
            {
                return user.OrderByDescending(s => propertyInfo.GetValue(s, null)).ToList();
            }
        }


        public async Task<IActionResult> CreateOrEdit(int? ID)
        {
            ViewBag.Title = ID == null ? "Create User" : "Edit User";
            if (ID != null)
            {
                var user = await _apiProvider.GetUserByID(ID);
                var result = user.Result;

                if (result != null)
                {
                    return View(result);
                }
                return NotFound();
            }
            else
            {
                return View(new User1());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(User1 user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _apiProvider.CreateOrEdit(user);
                    var results = result.StatusCode;
                    if (results != 200)
                    {
                        return View(user);
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return View(new User1());
            }
        }

        public async Task<IActionResult> Details(int ID)
        {
            var user = await _apiProvider.GetUserByID(ID);
            var result = user.Result;
            if (result != null)
            {
                //users.Gender = users.Gender.Trim();
                return View(result);
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(int ID)
        {
            if (ID.ToString() == null)
            {
                return NotFound();
            }
            else
            {
                var users = await _apiProvider.GetUserByID(ID);
                if (users != null)
                {
                    var result = _apiProvider.DeleteUser(ID);
                    var results = result.Result.StatusCode;
                    if (results != 200)
                    {
                        ViewBag.ErrorMessage = "User Not Deleted...!!!";
                    }
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
        }
    }
}
