using AutoMapper;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;

namespace EntrenatePues.Web.Mappings.Code
{
    public class CodeProfile : Profile
    {
        public CodeProfile() 
        {
            CreateMap<CodeGeneratorRequestDto, CodeGenerator>();
        }
    }
}
