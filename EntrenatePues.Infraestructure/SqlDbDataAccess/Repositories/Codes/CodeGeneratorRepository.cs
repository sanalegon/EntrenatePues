using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Interfaces.Helpers;
using EntrenatePues.Core.Interfaces.Repositories.Codes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrenatePues.Infraestructure.SqlDbDataAccess.Repositories.Codes
{
    public class CodeGeneratorRepository : ICodeGeneratorRepository
    {
        private readonly IStoreProcedureHelper _storeProcedureHelper;

        public CodeGeneratorRepository(IStoreProcedureHelper storeProcedureHelper, IConfiguration configuration)
        {
            SettingDatabaseConnection = configuration.GetConnectionString("SettingsDataBase");
            _storeProcedureHelper = storeProcedureHelper;
        }

        public string SettingDatabaseConnection { get; }

        public CodeGenerator GetCode(string code)
        {
            try
            {
                string sqlParameter = "SP_GET_CODE";
                IEnumerable<CodeGenerator> result = _storeProcedureHelper.ExecuteSp<CodeGenerator, object>(sqlParameter, new { Code = code }, SettingDatabaseConnection);

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool InsertCode(CodeGenerator codeGenerator)
        {
            try
            {
                string sqlParameter = "SP_INSERT_CODE";
                IEnumerable<bool> result = _storeProcedureHelper.ExecuteSp<bool, object>(sqlParameter,
                    new
                    {
                        UserId = codeGenerator.UserId,
                        Codigo = codeGenerator.Codigo,
                        CreationDate = codeGenerator.CreationDate,
                        ExpirationDate = codeGenerator.ExpirationDate,
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
