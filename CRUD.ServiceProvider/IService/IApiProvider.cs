using CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.ServiceProvider.Methods
{
    public interface IApiProvider
    {
        public IEnumerable<User1> GetUser();
        public User1 GetUserByID(int? id);
        public Task CreateOrEdit(User1 user1);
        public Task DeleteUser(int? id);
        //public IEnumerable<User1> SortUserData(string sortField, string currentSortField, string currentSortOrder);
    }
}
