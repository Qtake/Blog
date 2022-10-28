using System.ComponentModel.DataAnnotations;

namespace Blog.Service.DTOs
{
    public class ArticleRequest
    {
        [MaxLength(20)]
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
