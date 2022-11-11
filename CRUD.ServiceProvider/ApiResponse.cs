using CRUD_Application.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CRUD.ServiceProvider
{
    public abstract class ApiResponse
    {
        private IHttpContextAccessor _httpContextAccessor;
        public ApiResponse(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private const string BaseAddress = "https://localhost:7080/";
        public async Task<ApiResult<List<T>>> GetAPI<T>(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var stringResult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResult<List<T>>>(stringResult);
                }
                return null;
            }
        }

        public async Task<ApiResult<T>> GetAPIByID<T>(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var stringResult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResult<T>>(stringResult);
                }
                return null;
            }
        }

        public async Task<ApiResult<T>> GetAPIByEmail<T>(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                //var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var stringResult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResult<T>>(stringResult);
                }
                return null;
            }
        }

        public async Task<ApiResult<T>> PostApi<T>(string controllerName, string actionName, T model)
        {
            using (var client = new HttpClient())
            {
                var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseAddress);
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync(requestUri, content);

                string apiResponse = await postTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResult<T>>(apiResponse);
            }
        }

        public static async Task<T> PostAuth<T>(string controllerName, string actionName, T model)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseAddress);
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync(requestUri, content);

                string token = await postTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(token);
            }
        }

        //public async Task<ApiResult<T>> PostImage<T>(string controllerName, string actionName, Register model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        //byte[]? data;
        //        var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StreamContent(model.ImageFile.OpenReadStream()), "ProfileImage", Path.GetFileName(model.ImageFile.FileName));
        //        content.Add(new StringContent(model.UserName), "UserName");

        //        var fileContent = new StreamContent(new FileStream(uploadFilePath, FileMode.Open));
        //        fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileContentType);
        //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //        {
        //            Name = "File",
        //            FileName = "Text.txt"
        //        };
        //        content.Add(fileContent);

        //        var fileContent = new StreamContent(new FileStream(model.ImageFile.FileName, FileMode.Open));
        //        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //        {
        //            Name = "ProfileImage",
        //            FileName = model.ImageFile.FileName
        //        };
        //        content.Add(fileContent);

        //        client.BaseAddress = new Uri(BaseAddress);
        //        string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

        //        //HTTP POST
        //        var postTask = await client.PostAsync(requestUri, content);

        //        string apiResponse = await postTask.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<ApiResult<T>>(apiResponse);
        //    }
        //}
    }
}
