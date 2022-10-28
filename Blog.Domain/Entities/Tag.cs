namespace Blog.Domain.Entities
{
    public class Tag : EntityBase
    {
        public string Name { get; set; } = null!;
        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}
