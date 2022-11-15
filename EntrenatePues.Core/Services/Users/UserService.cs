using AutoMapper;
using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Repositories.Users;
using EntrenatePues.Core.Interfaces.Services.Codes;
using EntrenatePues.Core.Interfaces.Services.Mail;
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
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IMailService _mailService;

        public UserService(IUserRepository userRepository, IMapper mapper, ICodeGeneratorService codeGeneratorService, IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _codeGeneratorService = codeGeneratorService;
            _mailService = mailService;
        }

        public ResponseCode CangePasswordRecovery(ChangePasswordRecoveryRequestDto changePassword)
        {
            if (string.IsNullOrEmpty(changePassword.Code))
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "You must enter a code");
            }

            if (string.IsNullOrEmpty(changePassword.NewPassword))
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "You must enter a password");
            }

            CodeGenerator codeGenerator = _codeGeneratorService.GetCode(changePassword.Code);

            if (codeGenerator == null)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "Invalid or missing code");
            }

            User user = _userRepository.FindUserById(codeGenerator.UserId);
            if (codeGenerator == null)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "User does not exist or was deleted");
            }

            DateTime currentDate = DateTime.Now;
            int resultCompare = DateTime.Compare(currentDate, codeGenerator.ExpirationDate);
            bool response;
            if (resultCompare < 0 || resultCompare == 0)
            {
                response = _userRepository.UpdatePassword(user.Id, changePassword.NewPassword);
            }
            else
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "The code you entered has expired, please generate a new one and try again.");
            }

            return response ? new ResponseCode(HttpStatusCode.OK, "Password successfully changed") : new ResponseCode(HttpStatusCode.BadRequest, "Error updating password");
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

        public UserDto FindUserByEmail(string email)
        {
            return _mapper.Map<UserDto>(_userRepository.FindUserByeEmail(email));
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
            User currentUser = _userRepository.FindUserByeEmail(recoverPasswordRequest.Email);

            if (currentUser == null)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "Invalid email or does not exist");
            }

            string SafeCode = _codeGeneratorService.GenerateSafeCode();
            DateTime currenDate = DateTime.Now;
            DateTime expirationCode = currenDate.AddMinutes(3);

            CodeGeneratorRequestDto codeGenerator = new CodeGeneratorRequestDto
            {
                UserId = currentUser.Id,
                Codigo = SafeCode,
                CreationDate = currenDate,
                ExpirationDate = expirationCode
            };

            bool resultInsert = _codeGeneratorService.InsertCode(codeGenerator);
            ResponseCode response = new ResponseCode(HttpStatusCode.BadRequest, "The email could not be sent, please try again.");

            if (resultInsert)
            {
                string bodyEmail = "<p><strong>Secure code</strong></p><br>";
                bodyEmail += "<p>This is your verification code to recover your password.</p><br>";
                bodyEmail += $"<p>{SafeCode}</p>";

                response = _mailService.SendMail(currentUser.Email, "Recover Password Code", bodyEmail, currentUser.FullName);
            }

            return response;
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
