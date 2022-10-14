using CRUD.Models;
using CRUD.ServiceProvider.Methods;
using CRUD_Application.Models;

namespace CRUD.ServiceProvider
{
    public class ApiProvider : IApiProvider
    {

        public Task<ApiResult<List<User1>>> GetUser()
        {
            var result = ApiResponse<User1>.GetAPI("User", "GetUsers");
            return result;
        }

        public Task<ApiResult<User1>> GetUserByID(int? id)
        {
            var user = ApiResponse<User1>.GetAPIByID("User", "GetUserByID","?id="+ id);
            return user;
        }

        public Task CreateOrEdit(User1 user1)
        {
            var user = ApiResponse<User1>.PostAPI("User", "AddOrUpdateUser", user1);
            return user;
        }

        public Task DeleteUser(int? id)
        {
            var user = ApiResponse<User1>.GetAPI("User", "DeleteUser", "?id=" + id);
            return Task.CompletedTask;
        }
    }
}
