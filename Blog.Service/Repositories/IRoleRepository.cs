using Blog.Domain.Entities;
using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetAsync(RoleTypeEnum role);
    }
}
