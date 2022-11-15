using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Interfaces.Helpers;
using EntrenatePues.Core.Interfaces.Repositories.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrenatePues.Infraestructure.SqlDbDataAccess.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IStoreProcedureHelper _storeProcedureHelper;

        public UserRepository(IStoreProcedureHelper storeProcedureHelper, IConfiguration configuration)
        {
            SettingDatabaseConnection = configuration.GetConnectionString("SettingsDataBase");
            _storeProcedureHelper = storeProcedureHelper;
        }

        public string SettingDatabaseConnection { get; }

        public bool Create(User user)
        {
            try
            {
                string sqlParameter = "SP_INSERT_USER";
                IEnumerable<bool> result = _storeProcedureHelper.ExecuteSp<bool, object>(sqlParameter,
                    new
                    {
                        NOMBRE_COMPLETO = user.FullName,
                        CORREO = user.Email,
                        CELULAR = user.CellphoneNumber,
                        ROL = user.Role,
                        CONTRASENA = user.Password
                    },
                    SettingDatabaseConnection);

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                string sqlParameter = "SP_DELETE_USER";
                IEnumerable<bool> result = _storeProcedureHelper.ExecuteSp<bool, object>(sqlParameter, new { ID_USUARIO = id }, SettingDatabaseConnection);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public User FindUserByeEmail(string email)
        {
            try
            {
                string sqlParameter = "SP_GET_BY_EMAIL_USER";
                IEnumerable<UserResponse> result = _storeProcedureHelper.ExecuteSp<UserResponse, object>(sqlParameter, new { EMAIL = email }, SettingDatabaseConnection);
                User searchUser = null;
                foreach (UserResponse item in result)
                {
                    searchUser = new User
                    {
                        Id = item.IdUsuario,
                        FullName = item.NombreCompleto,
                        Email = item.Correo,
                        CellphoneNumber = item.Celular,
                        Role = item.Rol
                    };
                }
                return searchUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public User FindUserById(int id)
        {
            try
            {
                string sqlParameter = "SP_GET_BY_ID_USER";
                IEnumerable<UserResponse> result = _storeProcedureHelper.ExecuteSp<UserResponse, object>(sqlParameter, new { Id_User = id }, SettingDatabaseConnection);
                User searchUser = null;
                foreach (UserResponse item in result)
                {
                    searchUser = new User
                    {
                        Id = item.IdUsuario,
                        FullName = item.NombreCompleto,
                        Email = item.Correo,
                        CellphoneNumber = item.Celular,
                        Role = item.Rol
                    };
                }

                return searchUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                string sqlParameter = "SP_GET_ALL_USERS";
                List<User> users = new();
                IEnumerable<UserResponse> result = _storeProcedureHelper.ExecuteSp<UserResponse>(sqlParameter, SettingDatabaseConnection);

                foreach (UserResponse item in result)
                {
                    User user = new()
                    {
                        Id = item.IdUsuario,
                        FullName = item.NombreCompleto,
                        Email = item.Correo,
                        CellphoneNumber = item.Celular,
                        Role = item.Rol
                    };

                    users.Add(user);
                }

                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Update(User user)
        {
            try
            {
                string sqlParameter = "SP_UPDATE_USER";
                IEnumerable<bool> result = _storeProcedureHelper.ExecuteSp<bool, object>(sqlParameter,
                    new
                    {
                        ID_USUARIO = user.Id,
                        NOMBRE_COMPLETO = user.FullName,
                        CORREO = user.Email,
                        CELULAR = user.CellphoneNumber
                    },
                    SettingDatabaseConnection);

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdatePassword(int UserId, string password)
        {
            try
            {
                string sqlParameter = "SP_UPDATE_PASSWORD";
                IEnumerable<bool> result = _storeProcedureHelper.ExecuteSp<bool, object>(sqlParameter, new { ID_USUARIO = UserId, NUEVA_CONTRASENA = password }, SettingDatabaseConnection);

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
