using System.ComponentModel.DataAnnotations;

namespace EntrenatePues.Web.Models.Users
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Id is required to update user")]
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage ="You must enter a valid email address")]
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
    }
}
