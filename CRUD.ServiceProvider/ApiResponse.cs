using CRUD.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CRUD.ServiceProvider
{
    public class ApiResponse<T>
    {
        public static async Task<ApiResult<List<T>>> GetAPI(string controllerName, string actionName, string qString = "" )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/");
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var stringResult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResult<List<T>>>(stringResult);
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<ApiResult<T>> GetAPIByID(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/");
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

        public async static Task<ApiResult<T>> PostAPI(string controllerName, string actionName, T model)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7080/");
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync( requestUri, content);

                string apiResponse = await postTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResult<T>>(apiResponse);
            }
        }
    }
}
