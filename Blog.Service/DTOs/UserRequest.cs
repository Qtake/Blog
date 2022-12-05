﻿using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Service.DTOs
{
    public class UserRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatedPassword { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
