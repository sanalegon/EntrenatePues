using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class RecoverPasswordRequest
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "The field must be a valid email address")]
        public string Email { get; set; }
    }
}
