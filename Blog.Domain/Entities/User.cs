namespace Blog.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid RoleID { get; set; }
        public Role Role { get; set; } = null!;

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
