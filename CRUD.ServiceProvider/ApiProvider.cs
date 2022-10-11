using CRUD.ServiceProvider.Methods;
using CRUD_Application.Models;

namespace CRUD.ServiceProvider
{
    public class ApiProvider : IApiProvider
    {

        public IEnumerable<User1> GetUser()
        {
            var result = ApiResponse<User1>.GetAPI("User", "GetUsers");
            return result;
        }

        //IEnumerable<User1> IApiProvider.SortUserData(string sortField, string currentSortField, string currentSortOrder)
        //{
        //    if (string.IsNullOrEmpty(currentSortField))
        //    {
        //        currentSortField = "?currentSortField=Id";
        //    }
        //    else
        //    {
        //        currentSortField = "?currentSortField=" + currentSortField;
        //    }
        //    var result = ApiResponse<User1>.PostSortingAPI("User", "SortUserData", "?sortField=" + sortField, currentSortField, "?currentSortOrder=" + currentSortOrder);
        //    return (IEnumerable<User1>)result;
        //}

        public User1 GetUserByID(int? id)
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
