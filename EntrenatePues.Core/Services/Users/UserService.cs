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
            throw new NotImplementedException();
        }

        public ResponseCode Create(UserDto userDto)
        {
            userDto.Role = 2;

            return _userRepository.Create(_mapper.Map<User>(userDto)) ?
               new ResponseCode(HttpStatusCode.Created, "User created successfully") :
               new ResponseCode(HttpStatusCode.BadRequest, "Error creating user: The user could not be inserted because it already exists email or incorrect data");
        }

        public ResponseCode Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
