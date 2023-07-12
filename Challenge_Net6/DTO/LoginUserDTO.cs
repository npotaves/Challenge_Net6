using System.ComponentModel.DataAnnotations;

namespace Challenge_Net6.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
