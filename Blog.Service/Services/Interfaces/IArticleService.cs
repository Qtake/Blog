using Blog.Service.DTOs;

namespace Blog.Service.Services.Interfaces
{
    public interface IArticleService : IBaseService<ArticleRequest, ArticleResponse>
    {
        Task<ArticleResponse> GetUserByName(string name);
    }
}
