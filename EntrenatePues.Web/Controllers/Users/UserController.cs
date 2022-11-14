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
        public IActionResult Create([FromBody] UserRequest userRequest)
        {

            if (userRequest == null)
            {
                return BadRequest(new ResponseCode(HttpStatusCode.BadRequest, "User model invalid"));
            }

            UserDto userDto = _mapper.Map<UserDto>(userRequest);
            ResponseCode response = _userService.Create(userDto);
            return response.Status == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
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

        [Route("update")]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateUserRequest userUpdate)
        {
            ResponseCode response = _userService.Update(_mapper.Map<UserDto>(userUpdate));
            return response.Status == HttpStatusCode.OK ? Ok(response) : NotFound(response);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ResponseCode response = _userService.Delete(id);
            return response.Status == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            ResponseCode response = _userService.ChangePassword(_mapper.Map<ChangePasswordRequestDto>(changePasswordRequest));
            return response.Status == HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}
