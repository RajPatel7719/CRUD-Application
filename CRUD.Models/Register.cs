using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CRUD_Application.Models
{
    public class Register
    {
        public string? Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "User Name Is Required")]
        [DisplayName("User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("Password", ErrorMessage = "Confirm Password Doesn't Match With Passowrd")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IFormFile? ImageFile { get; set; }
    }
}
