using Blog.Service.DTOs;

namespace Blog.Service.Services.Interfaces
{
    public interface IUserService : IBaseService<UserRequest, UserResponse>
    {
        Task<UserResponse?> GetByEmailAsync(string email);
        Task Authenticate(UserRequest request);
        Task Registration();
        Task LogOut();
    }
}
