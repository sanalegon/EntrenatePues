using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class UserRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Password field is required for to create user")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required to update user")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address")]
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
    }
}
