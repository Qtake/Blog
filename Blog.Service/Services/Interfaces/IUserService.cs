using Blog.Service.DTOs;
using System.Linq.Expressions;

namespace Blog.Service.Services.Interfaces
{
    public interface IUserService : IBaseService<UserRequest, UserResponse>
    {
        Task<UserResponse?> GetByEmailAsync(string email);
        Task Authenticate(UserRequest request);
        Task<bool> Registration(UserRequest request);
        Task<bool> LogIn(UserRequest request);
        Task LogOut();
    }
}
