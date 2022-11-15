namespace EntrenatePues.Core.Dtos
{
    public class ChangePasswordRecoveryRequestDto
    {
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
