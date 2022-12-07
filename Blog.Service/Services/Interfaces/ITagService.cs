using Blog.Domain.Entities;
using Blog.Service.DTOs;

namespace Blog.Service.Services.Interfaces
{
    public interface ITagService : IBaseService<TagRequest, TagResponse>
    {
        Task<bool> ExistByName(string name);
        Task<Tag?> GetByNameAsync(string name);
    }
}
