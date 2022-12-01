using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Models;
using Blog.Service.Repositories;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IUserRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task Authenticate(UserRequest request)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, request.Name)
            };

            var id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        public async Task<bool> Registration(RegistrationModel model)
        {
            UserRequest request = _mapper.Map<UserRequest>(model);

            bool isExist = await _repository.Exist(x => x.Name == request.Name && x.Email == request.Email);

            if (isExist)
            {
                return false;
            }

            await AddAsync(request);
            await Authenticate(request);

            return true;

        }

        public async Task<bool> LogIn(AuthorizationModel model)
        {
            UserRequest request = _mapper.Map<UserRequest>(model);

            bool isExist = await _repository.Exist(x => x.Email == x.Email && x.Password == request.Password);

            if (!isExist)
            {
                return false;
            }

            await Authenticate(request);

            return true;
        }

        public async Task LogOut()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IQueryable<UserResponse>> GetAllAsync()
        {
            IQueryable<User> list = await _repository.GetAllAsync();

            return list.Select(x => _mapper.Map<UserResponse>(x));
        }

        public async Task<UserResponse?> GetAsync(Guid id)
        {
            User? entity = await _repository.GetAsync(id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse?> GetByEmailAsync(string email)
        {
            User? entity = await _repository.GetAsync(x => x.Email == email);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<Guid> AddAsync(UserRequest request)
        {
            User entity = _mapper.Map<User>(request);

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, UserRequest request)
        {
            try
            {
                User entity = _mapper.Map<User>(request);
                await _repository.UpdateAsync(id, entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                await _repository.RemoveAsync(id);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
