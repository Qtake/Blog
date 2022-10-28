using Blog.Domain.Entities;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;

namespace Blog.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BlogContext context) : base(context)
        {
        }
    }
}
