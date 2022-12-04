using EntrenatePues.Core.Domain;
using System.Collections.Generic;

namespace EntrenatePues.Core.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        bool Create(User user);
        IEnumerable<User> GetAll();
        User FindUserById(int id);
        User FindUserByeEmail(string email);
        bool Update(User user);
        bool Delete(int id);
        bool UpdatePassword(int UserId, string password);
        bool ValidatePassword(string password);
    }
}
