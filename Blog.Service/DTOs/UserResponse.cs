using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Service.DTOs
{
    public class UserResponse : EntityBase
    {
        public string Name { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
