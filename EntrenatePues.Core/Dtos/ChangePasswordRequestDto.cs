namespace EntrenatePues.Core.Dtos
{
    public class ChangePasswordRequestDto
    {
        public int IdUser { get; set; }
        public string NewPassword { get; set; }
    }
}
