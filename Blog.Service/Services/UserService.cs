using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Service.DTOs;
using Blog.Service.Models;
using Blog.Service.Repositories;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _roleRepository = roleRepository;
        }

        public async Task Authenticate(UserRequest request)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, request.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, request.Role.Name)
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

            bool isNameExist = await _userRepository.Exist(x => x.Name == request.Name);
            bool isEmailExist = await _userRepository.Exist(x => x.Email == request.Email);

            if (isNameExist || isEmailExist)
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

            User? user = await _userRepository.GetAsync(
                x => x.Name == request.Name && x.Password == request.Password, p => p.Role);

            if (user is null)
            {
                return false;
            }

            request.Role = user.Role;

            await Authenticate(request);

            return true;
        }

        public async Task LogOut()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IQueryable<UserResponse>> GetAllAsync()
        {
            IQueryable<User> query = await _userRepository.GetAllAsync(x => x.Role);

            return query.Select(x => _mapper.Map<UserResponse>(x));
        }

        public async Task<UserResponse?> GetAsync(Guid id)
        {
            User? entity = await _userRepository.GetAsync(id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse?> GetByEmailAsync(string email)
        {
            User? entity = await _userRepository.GetAsync(x => x.Email == email);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse?> GetByNameAsync(string name)
        {
            User? entity = await _userRepository.GetAsync(x => x.Name == name);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<Guid> AddAsync(UserRequest request)
        {
            User entity = _mapper.Map<User>(request);
            Role role = await _roleRepository.GetAsync(RoleType.User);
            entity.Role = role;

            return await _userRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, UserRequest request)
        {
            bool isExist = await _userRepository.Exist(x => x.ID == id);

            if (!isExist)
            {
                return false;
            }

            User entity = _mapper.Map<User>(request);
            await _userRepository.UpdateAsync(id, entity);

            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            bool isExist = await _userRepository.Exist(x => x.ID == id);

            if (!isExist)
            {
                return false;
            }

            return true;
        }
    }
}
