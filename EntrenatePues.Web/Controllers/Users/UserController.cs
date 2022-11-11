using AutoMapper;
using EntrenatePues.Core.Common.Responses;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Core.Interfaces.Services.Users;
using EntrenatePues.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Route("create")]
        [HttpPost]
        public ResponseCode Create([FromBody] UserRequest userRequest)
        {

            if (userRequest == null)
            {
                return new ResponseCode(HttpStatusCode.BadRequest, "User model invalid");
            }

            UserDto userDto = _mapper.Map<UserDto>(userRequest);
            return _userService.Create(userDto);
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<UserDto> userDto = _userService.GetAll();
            IEnumerable<UserResponseDto> userResponseDtos = _mapper.Map<IEnumerable<UserResponseDto>>(userDto);
            return userResponseDtos == null ? NotFound(new ResponseCode(HttpStatusCode.NotFound, "Users not found")) : Ok(userResponseDtos);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetByUserId(int id)
        {
            if (id == 0)
            {
                return BadRequest(new ResponseCode(HttpStatusCode.BadRequest, "Invalid id"));
            }

            UserDto userDto = _userService.FindUserByUserId(id);
            UserResponseDto userResponseDto = _mapper.Map<UserResponseDto>(userDto);
            return userResponseDto == null ? NotFound(new ResponseCode(HttpStatusCode.NotFound, "User not Found")) : Ok(userResponseDto);
        }
    }
}
