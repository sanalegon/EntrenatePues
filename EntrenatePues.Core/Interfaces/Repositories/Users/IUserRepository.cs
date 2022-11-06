using EntrenatePues.Core.Domain;

namespace EntrenatePues.Core.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        bool Create(User user);
    }
}
