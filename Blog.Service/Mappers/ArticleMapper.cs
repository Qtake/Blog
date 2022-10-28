using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;

namespace Blog.Service.Mappers
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper()
        {
            CreateMap<ArticleRequest, Article>();

            CreateMap<Article, ArticleResponse>();
        }
    }
}
