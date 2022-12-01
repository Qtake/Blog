using Blog.Domain.Entities;
using Blog.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entitySet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entitySet = _context.Set<TEntity>();
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_entitySet.AsQueryable());
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entitySet.FirstOrDefaultAsync(predicate);
        }

        public async Task<Guid> AddAsync(TEntity entity)
        {
            var result = await _entitySet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return result.Entity.ID;
        }

        public async Task UpdateAsync(Guid id, TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _entitySet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = new TEntity() { ID = id };

            await RemoveAsync(entity);
        }

        public async Task<bool> Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entitySet.AnyAsync(predicate);
        }
    }
}
