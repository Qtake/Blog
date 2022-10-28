using Blog.Domain.Entities;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;

namespace Blog.Infrastructure.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(BlogContext context) : base(context)
        {
        }
    }
}
