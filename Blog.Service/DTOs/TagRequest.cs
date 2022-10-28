using System.ComponentModel.DataAnnotations;

namespace Blog.Service.DTOs
{
    public class TagRequest
    {
        [MaxLength(20)]
        public string Name { get; set; } = null!;
    }
}
