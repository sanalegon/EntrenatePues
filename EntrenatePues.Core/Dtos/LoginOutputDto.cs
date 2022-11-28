using System;

namespace EntrenatePues.Core.Dtos
{
    public class LoginOutputDto
    {
        public string AuthToken { get; }

        public DateTime ExpiresIn { get; }

        public LoginOutputDto(string token, DateTime expiresIn)
        {
            AuthToken = token;
            ExpiresIn = expiresIn;
        }
    }
}
