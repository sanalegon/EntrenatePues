using AutoMapper;
using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Services.Users;
using EntrenatePues.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EntrenatePues.Web.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public ResponseCode Create([FromBody] UserRequest userRequest)
        {

            if (userRequest == null)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "User model invalid");
            }

            UserDto userDto = _mapper.Map<UserDto>(userRequest);
            return _userService.Create(userDto);
        }
    }
}
