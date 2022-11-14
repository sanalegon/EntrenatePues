using AutoMapper;
using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Repositories.Users;
using EntrenatePues.Core.Interfaces.Services.Users;
using System;
using System.Collections.Generic;
using System.Net;

namespace EntrenatePues.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ResponseCode ChangePassword(ChangePasswordRequestDto changePasswordRequest)
        {
            if (string.IsNullOrEmpty(changePasswordRequest.NewPassword))
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "You must enter a password");
            }

            return _userRepository.UpdatePassword(changePasswordRequest.IdUser, changePasswordRequest.NewPassword) ?
                    new ResponseCode(HttpStatusCode.OK, "Password Updated") :
                    new ResponseCode(HttpStatusCode.BadRequest, "Error Updating Password");
        }

        public ResponseCode Create(UserDto userDto)
        {
            userDto.Role = 2;

            return _userRepository.Create(_mapper.Map<User>(userDto)) ?
               new ResponseCode(HttpStatusCode.OK, "User created successfully") :
               new ResponseCode(HttpStatusCode.BadRequest, "Error creating user: The user could not be inserted because it already exists email or incorrect data");
        }

        public ResponseCode Delete(int id)
        {
            if (id == 0)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "Invalid Id");
            }

            return _userRepository.Delete(id) ?
                       new ResponseCode(HttpStatusCode.OK, "User removed successfully") :
                       new ResponseCode(HttpStatusCode.BadRequest, "The user you want to delete does not exist");
        }

        public UserDto FindUserByUserId(int userId)
        {
            return _mapper.Map<UserDto>(_userRepository.FindUserById(userId));
        }
        
        public IEnumerable<UserDto> GetAll()
        {
           return _mapper.Map<IEnumerable<UserDto>>(_userRepository.GetAll());
        }

        public ResponseCode RecoverPassword(RecoverPasswordRequestDto recoverPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public ResponseCode Update(UserDto userDto)
        {
            if (userDto.Id == 0)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "Invalid user id");
            }

            return _userRepository.Update(_mapper.Map<User>(userDto)) ?
               new ResponseCode(HttpStatusCode.OK, "User Updated") :
               new ResponseCode(HttpStatusCode.NotFound, "Error: user not found or does not exist");
        }
    }
}
