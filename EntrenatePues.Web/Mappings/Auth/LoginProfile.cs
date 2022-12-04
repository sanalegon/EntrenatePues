using AutoMapper;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Web.Models.Auth;

namespace EntrenatePues.Web.Mappings.Auth
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginRequest, LoginRequestDto>();
        }
    }
}
