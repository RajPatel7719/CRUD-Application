using CRUD_Application.Models;
using CRUD.ServiceProvider.IService;
using Microsoft.AspNetCore.Http;

namespace CRUD.ServiceProvider
{
    public class ApiProvider : ApiResponse, IApiProvider
    {
        public ApiProvider(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        public Task<ApiResult<List<User1>>> GetUser()
        {
            var result = GetAPI<User1>("User", "GetUsers");
            return result;
        }

        public Task<ApiResult<User1>> GetUserByID(int? id)
        {
            var user = GetAPIByID<User1>("User", "GetUserByID", "?id="+ id);
            return user;
        }

        public Task<ApiResult<User1>> CreateOrEdit(User1 user1)
        {
            var user = PostApi("User", "AddOrUpdateUser", user1);
            return user;
        }

        public Task<ApiResult<User1>> DeleteUser(int? id)
        {
            var user = GetAPIByID<User1>("User", "DeleteUser", "?id=" + id);
            return user;
        }

        public Task<Login> Login(Login login)
        {
            var loginUser = PostAuth("Authentication", "Login", login);
            return loginUser;
        }
        public Task<Register> Register(Register register)
        {
            var registerUser = PostAuth("Authentication", "Register", register);
            return registerUser;
        }

        public Task EditProfile(Register register)
        {
            return PostApi("Authentication", "EditProfile", register);
        }

        public Task<ApiResult<Register>> GetUserByEmail(string email)
        {
            var user = GetAPIByEmail<Register>("Authentication", "GetUserByEmail", "?email=" + email);
            return user;
        }
    }
}
