using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public string ProfilePicture { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IFormFile? ImageFile { get; set; }
    }
}
