using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Service.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetAsync(RoleType role);
    }
}
