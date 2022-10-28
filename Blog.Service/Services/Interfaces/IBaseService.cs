using System.Linq.Expressions;

namespace Blog.Service.Services.Interfaces
{
    public interface IBaseService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse?> GetAsync(Guid id);
        //Task<TResponse?> GetAsync(Expression<Func<TRequest, bool>> predicate);
        Task<Guid> AddAsync(TRequest request);
        Task<bool> UpdateAsync(Guid id, TRequest request);
        Task<bool> RemoveAsync(Guid id);
    }
}
