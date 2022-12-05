using Blog.Domain.Entities;

namespace Blog.Service.DTOs
{
    public class ArticleResponse : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Tag> Tags { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}
