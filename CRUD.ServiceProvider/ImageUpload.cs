using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using CRUD.ServiceProvider.IService;
using CRUD_Application.Models;

namespace CRUD.ServiceProvider
{
    public class ImageUpload : IImageUpload
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IApiProvider _apiProvider;
        public ImageUpload(IHostingEnvironment hostingEnvironment, IApiProvider apiProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _apiProvider = apiProvider;
        }
        public async Task<string> SaveImage(IFormFile imageFile, string userName)
        {
            string imageName = "";
            if (imageFile != null)
            {
                try
                {
                    if (imageFile.Length > 0)
                    {
                        imageName = GetUniqueFileName(imageFile.FileName, userName);

                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath + "\\images\\", imageName);

                        if (!Directory.Exists(_hostingEnvironment.WebRootPath))
                        {
                            Directory.CreateDirectory(_hostingEnvironment.WebRootPath);
                        }

                        if (!File.Exists(imagePath))
                        {
                            using (FileStream fileStream = new(imagePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            File.Delete(imagePath);
                            using (FileStream fileStream = new(imagePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("File Copy Failed", ex);
                }
            }
            return imageName;
        }

        public async Task<FileStream> GetImage(string ImageName)
        {
            FileStream? imageFileStream = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", $"{ImageName}");
                imageFileStream = File.OpenRead(path);
                return imageFileStream;
            }
            return imageFileStream;
        }

        public async Task DeleteImage(string ImageName)
        {
            if (!string.IsNullOrEmpty(ImageName))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", ImageName);
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
        }

        public string GetUniqueFileName(string fileName, string email)
        {
            string imageName = "";
            var userdetail = _apiProvider.GetUserByEmail(email).Result.Result;
            imageName = userdetail.Id + ".png";
            return imageName;
        }
    }
}
