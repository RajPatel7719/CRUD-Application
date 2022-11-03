using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Application.Models.ModelsDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "User Name Is Required")]
        [DisplayName("User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = string.Empty;
        public bool TwoFactorEnabled { get; set; }
    }
}
