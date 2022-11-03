using CRUD_Application.Models;

namespace CRUD.ServiceProvider.IService
{
    public interface IApiProvider
    {
        public Task<ApiResult<List<User1>>> GetUser();
        public Task<ApiResult<User1>> GetUserByID(int? id);
        public Task<ApiResult<User1>> CreateOrEdit(User1 user1);
        public Task<ApiResult<User1>> DeleteUser(int? id);
        public Task<Login> Login(Login login);
        public Task<Register> Register(Register register);
        public Task<ApiResult<Register>> GetUserByEmail(string email);
        public Task EditProfile(Register register);
        public Task<ApiResult<List<Register>>> GetProfile();
    }
}
