using AutoMapper;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Repositories.Codes;
using EntrenatePues.Core.Interfaces.Services.Codes;
using System;
using System.Linq;

namespace EntrenatePues.Core.Services.Codes
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly IMapper _mapper;
        private readonly ICodeGeneratorRepository _codeGeneratorRepository;

        public CodeGeneratorService(IMapper mapper, ICodeGeneratorRepository codeGeneratorRepository)
        {
            _mapper = mapper;
            _codeGeneratorRepository = codeGeneratorRepository;
        }

        public string GenerateSafeCode()
        {
            string codeSafe = string.Empty;
            Enumerable.Range(48, 75)
              .Where(i => i is < 58 or > 64 and < 91 or > 96)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => codeSafe += Convert.ToChar(i));
            string code = codeSafe.Substring(new Random().Next(0, codeSafe.Length), new Random().Next(5, 7));

            return code;
        }

        public Domain.CodeGenerator GetCode(string code)
        {
            return _codeGeneratorRepository.GetCode(code);
        }

        public bool InsertCode(CodeGeneratorRequestDto codeGenerator)
        {
            return _codeGeneratorRepository.InsertCode(_mapper.Map<CodeGenerator>(codeGenerator));
        }
    }
}
