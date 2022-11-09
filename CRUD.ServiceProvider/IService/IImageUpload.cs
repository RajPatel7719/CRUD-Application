using Microsoft.AspNetCore.Http;

namespace CRUD.ServiceProvider.IService
{
    public interface IImageUpload
    {
        Task<string> SaveImage(IFormFile imageFile, string userName);
        Task DeleteImage(string imageFile);
        Task<FileStream> GetImage(string imageFile);
    }
}