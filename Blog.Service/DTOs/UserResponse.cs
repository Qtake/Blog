using Blog.Domain.Entities;

namespace Blog.Service.DTOs
{
    public class UserResponse : EntityBase
    {
        public string Name { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
