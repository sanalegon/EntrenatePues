using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class ChangePasswordRecoveryRequest
    {
        [Required(ErrorMessage = "Verification code is mandatory to change the password")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The password is mandatory to perform the process")]
        public string NewPassword { get; set; }
    }
}
