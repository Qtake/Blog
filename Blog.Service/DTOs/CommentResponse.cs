using Blog.Domain.Entities;

namespace Blog.Service.DTOs
{
    public class CommentResponse : EntityBase
    {
        public string Content { get; set; } = null!;
    }
}
