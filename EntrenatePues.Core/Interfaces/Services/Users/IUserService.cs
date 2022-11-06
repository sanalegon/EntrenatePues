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
        ResponseCode Update(UserDto userDto);
        ResponseCode Delete(int id);
        ResponseCode ChangePassword(ChangePasswordRequestDto changePasswordRequest);
        ResponseCode RecoverPassword(RecoverPasswordRequestDto recoverPasswordRequest);
    }
}
