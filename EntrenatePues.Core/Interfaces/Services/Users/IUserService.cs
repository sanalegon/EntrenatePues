using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Dtos;
using System.Collections.Generic;

namespace EntrenatePues.Core.Interfaces.Services.Users
{
    public interface IUserService
    {
        ResponseCode Create(UserDto userDto);
        IEnumerable<UserDto> GetAll();
        UserDto FindUserByUserId(int userId);
        UserDto FindUserByEmail(string email);
        ResponseCode Update(UserDto userDto);
        ResponseCode Delete(int id);
        ResponseCode ChangePassword(ChangePasswordRequestDto changePasswordRequest);
        ResponseCode RecoverPassword(RecoverPasswordRequestDto recoverPasswordRequest);
        ResponseCode CangePasswordRecovery(ChangePasswordRecoveryRequestDto changePassword);
        bool ValidatePassword(string password);
    }
}
