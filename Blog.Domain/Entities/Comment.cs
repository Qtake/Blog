namespace Blog.Domain.Entities
{
    public class Comment : EntityBase
    {
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
        public User User { get; set; } = null!;
    }
}
