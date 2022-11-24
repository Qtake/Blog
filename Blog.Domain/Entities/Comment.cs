namespace Blog.Domain.Entities
{
    public class Comment : EntityBase
    {
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
        public User User { get; set; } = null!;
        public Guid ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}
