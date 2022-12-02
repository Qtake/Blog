using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Infrastructure.Contexts;
using Blog.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(BlogContext context) : base(context)
        {
        }

        public async Task<Role> GetAsync(RoleTypeEnum role)
        {
            IQueryable<Role> query = await GetAllAsync();
            Role entity = await query.FirstAsync(x => x.Name == role.ToString());

            return entity;
        }
    }
}
