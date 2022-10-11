using Newtonsoft.Json;
using System.Text;

namespace CRUD.ServiceProvider
{
    public class ApiResponse<T>
    {
        public static List<T> GetAPI(string controllerName, string actionName, string qString = "" )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/");
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<T>>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return null;
                }
            }
        }

        public static T GetAPIByID(string controllerName, string actionName, string qString = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/");
                //HTTP GET
                var responseTask = client.GetAsync("api/" + controllerName + "/" + actionName + qString);

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                return JsonConvert.DeserializeObject<T>(null); 
            }
        }

        public async static Task<T> PostAPI(string controllerName, string actionName, T model)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7080/");
                string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName;

                //HTTP POST
                var postTask = await client.PostAsync( requestUri, content);

                string apiResponse = await postTask.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<T>(apiResponse);
                return user;
            }
        }
        //public static Task<T> PostSortingAPI(string controllerName, string actionName, string sortField, string currentSortField, string currentSortOrder)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7080/");
        //        //HTTP GET
        //        string requestUri = client.BaseAddress + "api/" + controllerName + "/" + actionName + sortField + currentSortField + currentSortOrder;


        //        var responseTask = client.GetAsync(requestUri);

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return Task.FromResult(JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result));
        //        }
        //        return Task.FromResult(JsonConvert.DeserializeObject<T>(null));
        //    }
        //}
    }
}
