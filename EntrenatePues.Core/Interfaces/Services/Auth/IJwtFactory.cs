using EntrenatePues.Core.Domain;

namespace EntrenatePues.Core.Interfaces.Services.Auth
{
    public interface IJwtFactory
    {
        public Token GenerateToken(User user);
    }
}
