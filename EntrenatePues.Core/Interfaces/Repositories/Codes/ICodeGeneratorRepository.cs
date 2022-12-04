using EntrenatePues.Core.Domain;

namespace EntrenatePues.Core.Interfaces.Repositories.Codes
{
    public interface ICodeGeneratorRepository
    {
        bool InsertCode(CodeGenerator codeGenerator);
        CodeGenerator GetCode(string code);
    }
}
