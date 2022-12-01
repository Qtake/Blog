using System.Linq.Expressions;

namespace Blog.Service.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(Guid id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<Guid> AddAsync(TEntity entity);
        Task UpdateAsync(Guid id, TEntity entity);
        Task RemoveAsync(Guid id);
        Task<bool> Exist(Expression<Func<TEntity, bool>> predicate);
    }
}
