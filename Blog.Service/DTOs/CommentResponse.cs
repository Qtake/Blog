using Blog.Domain.Entities;

namespace Blog.Service.DTOs
{
    public class CommentResponse : EntityBase
    {
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
        public User User { get; set; } = null!;
        public Guid ArticleId { get; set; }
    }
}
