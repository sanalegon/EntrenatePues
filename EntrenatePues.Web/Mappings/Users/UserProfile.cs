﻿using AutoMapper;
using EntrenatePues.Core.Domain;
using EntrenatePues.Core.Dtos;
using EntrenatePues.Web.Models.Users;

namespace EntrenatePues.Web.Mappings.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, UserDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, UserResponseDto>();
        }
    }
}
