using Blog.Service.DTOs;
using Blog.Service.Models;

namespace Blog.Service.Services.Interfaces
{
    public interface IUserService : IBaseService<UserRequest, UserResponse>
    {
        Task<UserResponse?> GetByEmailAsync(string email);
        Task<UserResponse?> GetByNameAsync(string name);
        Task Authenticate(UserRequest request);
        Task<bool> Registration(RegistrationModel model);
        Task<bool> LogIn(AuthorizationModel model);
        Task LogOut();
    }
}
