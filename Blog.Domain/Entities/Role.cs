namespace Blog.Domain.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
