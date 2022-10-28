using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;

namespace Blog.Service.Mappers
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<TagRequest, Tag>();

            CreateMap<Tag, TagResponse>();
        }
    }
}
