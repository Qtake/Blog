using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(BlogContext context) : base(context)
        {
        }

        public async Task<Role> GetAsync(RoleType role)
        {
            IQueryable<Role> query = await GetAllAsync();
            Role entity = await query.FirstAsync(x => x.Name == role.ToString());

            return entity;
        }
    }
}
