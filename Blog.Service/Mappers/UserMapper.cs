using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Models;

namespace Blog.Service.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AuthorizationModel, UserRequest>();

            CreateMap<RegistrationModel, UserRequest>();

            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>();
        }
    }
}
