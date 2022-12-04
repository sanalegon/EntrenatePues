using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;

namespace EntrenatePues.Core.Interfaces.Services.Codes
{
    public interface ICodeGeneratorService
    {
        string GenerateSafeCode();
        bool InsertCode(CodeGeneratorRequestDto codeGenerator);
        CodeGenerator GetCode(string code);

    }
}
