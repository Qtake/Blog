using System.ComponentModel.DataAnnotations;

namespace Blog.Service.Models
{
    public sealed class RegistrationModel
    {
        [MinLength(6)]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [Display(Name = "Repeated password")]
        [MinLength(6)]
        [MaxLength(20)]
        public string RepeatedPassword { get; set; } = null!;
    }
}
