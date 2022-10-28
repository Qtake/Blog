using System.ComponentModel.DataAnnotations;

namespace Blog.Service.DTOs
{
    public class CommentRequest
    {
        [MaxLength(300)]
        public string Content { get; set; } = null!;
    }
}
