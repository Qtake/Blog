using System.ComponentModel.DataAnnotations;

namespace Blog.Service.Models
{
    public class AuthorizationModel
    {
        [MinLength(6)]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; } = null!;
    }
}
