using CRUD_Application.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
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

        public async Task<ApiResult<List<T>>> GetAPI<T>(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/");
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
                client.BaseAddress = new Uri("https://localhost:7080/");
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

        public async Task<ApiResult<T>> PostApi<T>(string controllerName, string actionName, T model)
        {
            using (var client = new HttpClient())
            {
                var Token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7080/");
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync(requestUri, content);

                string apiResponse = await postTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResult<T>>(apiResponse);
            }
        }

        public static async Task<Login> PostAuth(string controllerName, string actionName, Login model)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7080/");
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync(requestUri, content);

                string token = await postTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Login>(token);
            }
        }
    }
}
