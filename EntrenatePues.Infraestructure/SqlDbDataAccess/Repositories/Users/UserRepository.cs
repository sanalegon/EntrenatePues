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
    }
}
