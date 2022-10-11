using System;
using System.Collections.Generic;

namespace CRUD_Application.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
