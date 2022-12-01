namespace Blog.Service.Services.Interfaces
{
    public interface IBaseService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<IQueryable<TResponse>> GetAllAsync();
        Task<TResponse?> GetAsync(Guid id);
        Task<Guid> AddAsync(TRequest request);
        Task<bool> UpdateAsync(Guid id, TRequest request);
        Task<bool> RemoveAsync(Guid id);
    }
}
