namespace Blog.Domain.Entities
{
    public class Article : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid UserID { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
