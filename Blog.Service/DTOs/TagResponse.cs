using Blog.Domain.Entities;

namespace Blog.Service.DTOs
{
    public class TagResponse : EntityBase
    {
        public string Name { get; set; } = null!;

        public ICollection<Article> Articles { get; set; } = null!;
    }
}
