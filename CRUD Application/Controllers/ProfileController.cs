using CRUD_Application.Models.ModelsDTO;
using CRUD_Application.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRUD.ServiceProvider.IService;
using Microsoft.Win32;

namespace CRUD_Application.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IApiProvider _apiProvider;
        private readonly IMapper _mapper;
        private readonly IImageUpload _imageUpload;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IApiProvider apiProvider,
                                 IMapper mapper,
                                 IImageUpload imageUpload,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _apiProvider = apiProvider;
            _mapper = mapper;
            _imageUpload = imageUpload;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _apiProvider.GetProfile();
            var result = user.Result;

            return View(result);
        }

        public async Task<IActionResult> EditProfile(string email)
        {
            var user = await _apiProvider.GetUserByEmail(email);
            if (user == null)
                return RedirectToActionPermanent("Index", "Profile");

            return View(user.Result);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(RegisterDTO user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedUser = _mapper.Map<Register>(user);
                    if (user.ImageFile != null)
                    {
                        var image = await _imageUpload.SaveImage(user.ImageFile, user.UserName);
                        if (!string.IsNullOrEmpty(image))
                        {
                            mappedUser.ProfilePicture = image;
                            await _apiProvider.EditProfile(mappedUser);
                            _httpContextAccessor.HttpContext.Session.Remove("ProfileImage");
                            _httpContextAccessor.HttpContext.Session.SetString("ProfileImage", image);
                        }
                    }
                    else
                    {
                        await _apiProvider.EditProfile(mappedUser);
                    }

                    return RedirectToActionPermanent("Index", "Profile");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View();
        }

        public IActionResult UploadProfilePicture()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadProfilePicture(Register user)
        //{
        //    var result = await _apiProvider.UploadProfilePicture(user);
        //    if (result.Result.Status == "Success")
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return View(user.Email);
        //}

        public IActionResult Skip()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
