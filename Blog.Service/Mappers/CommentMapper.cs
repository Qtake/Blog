using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;

namespace Blog.Service.Mappers
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<CommentRequest, Comment>();

            CreateMap<Comment, CommentResponse>();
        }
    }
}
