using Blog.Domain.Entities;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;

namespace Blog.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {
        }
    }
}
