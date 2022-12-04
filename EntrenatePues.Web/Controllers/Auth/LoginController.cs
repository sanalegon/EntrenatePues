using AutoMapper;
using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Services.Auth;
using EntrenatePues.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EntrenatePues.Web.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Auth")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            LoginOutputDto loginOutputDto = _authService.CreateToken(_mapper.Map<LoginRequestDto>(loginRequest));

            return loginOutputDto.AuthToken == null ? BadRequest(new ResponseCode(HttpStatusCode.BadRequest, "Incorrect email or password")) : Ok(loginOutputDto);
        }
    }
}
