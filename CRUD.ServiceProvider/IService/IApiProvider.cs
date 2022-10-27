using CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
