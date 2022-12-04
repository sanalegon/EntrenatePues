using EntrenatePues.Core.Dtos;

namespace EntrenatePues.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        public LoginOutputDto CreateToken(LoginRequestDto loginRequestDto);
    }
}
