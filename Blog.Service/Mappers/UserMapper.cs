using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;

namespace Blog.Service.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>();
        }
    }
}
