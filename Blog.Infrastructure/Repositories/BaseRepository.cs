using Blog.Domain.Entities;
using Blog.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _entitySet.AsQueryable();

            return Task.FromResult(query.Where(predicate));
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties.Length == 0)
            {
                return await GetAllAsync();
            }

            return ApplyIncludes(includeProperties);
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entitySet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties.Length == 0)
            {
                return await GetAsync(predicate);
            }

            return await ApplyIncludes(includeProperties).FirstOrDefaultAsync(predicate);
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

            //TEntity? existing = await GetAsync(id);

            //if (existing is null)
            //{
            //    return;
            //}

            //_context.Entry(existing).CurrentValues.SetValues(entity);
            //await _context.SaveChangesAsync();

            //await RemoveAsync(id);
            //await AddAsync(entity);
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

        private IQueryable<TEntity> ApplyIncludes(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entitySet.AsQueryable();

            return includeProperties.Aggregate(query, (current, property) => current.Include(property));
        }
    }
}
