using Blog.Domain.Entities;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;

namespace Blog.Infrastructure.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(BlogContext context) : base(context)
        {
        }
    }
}
