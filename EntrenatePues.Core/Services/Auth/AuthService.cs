using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Repositories.Users;
using EntrenatePues.Core.Interfaces.Services.Auth;
using System;

namespace EntrenatePues.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;

        public AuthService(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public LoginOutputDto CreateToken(LoginRequestDto loginRequestDto)
        {
            User user = _userRepository.FindUserByeEmail(loginRequestDto.Email);

            if (user is null || !_userRepository.ValidatePassword(loginRequestDto.Password))
            {
                return new LoginOutputDto(null, DateTime.Now);
            }

            Token token = _jwtFactory.GenerateToken(user);
            return new LoginOutputDto(token.AuthToken, token.ExpiresIn);

        }
    }
}
