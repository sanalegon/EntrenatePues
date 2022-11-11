using EntrenatePues.Core.Domain;
using System.Collections.Generic;

namespace EntrenatePues.Core.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        bool Create(User user);
        IEnumerable<User> GetAll();
        User FindUserById(int id);
    }
}
