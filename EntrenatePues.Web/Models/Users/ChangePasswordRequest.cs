using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class ChangePasswordRequest
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
