using System;

namespace EntrenatePues.Core.Domain
{
    public class Token
    {
        public Token(string authToken, DateTime expiresIn)
        {
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }

        public string AuthToken { get; }
        public DateTime ExpiresIn { get; }
    }
}
