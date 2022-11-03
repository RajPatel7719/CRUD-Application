using CRUD_Application.Models.ModelsDTO;
using CRUD_Application.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRUD.ServiceProvider.IService;

namespace CRUD_Application.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IApiProvider _apiProvider;
        private readonly IMapper _mapper;

        public ProfileController(IApiProvider apiProvider, IMapper mapper)
        {
            _apiProvider = apiProvider;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchString, int? pageNo)
        {
            //var Auth = HttpContext.Session.GetString("Token");
            var user = await _apiProvider.GetProfile();
            var result = user.Result;
            
            if (!String.IsNullOrEmpty(SearchString))
            {
                result = result.Where(s => s.Email.Contains(SearchString)).ToList();
            }
            int pageSize = 10;
            return View(result);
        }

        public async Task<IActionResult> EditProfile(string email)
        {
            var user = await _apiProvider.GetUserByEmail(email);
            if (user == null)
                return RedirectToActionPermanent("Index", "User");

            return View(user.Result);
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
