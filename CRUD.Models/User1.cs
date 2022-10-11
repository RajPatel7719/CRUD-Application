using System.ComponentModel.DataAnnotations;

namespace CRUD_Application.Models
{
    public partial class User1
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name Is Required")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name Is Required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Is Not Valid")]
        public string? Email { get; set; }
        [Required]
        public bool? Gender { get; set; }
    }
}
