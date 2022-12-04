using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Id user is required")]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "New password is mandatory for update password")]
        public string NewPassword { get; set; }

    }
}
