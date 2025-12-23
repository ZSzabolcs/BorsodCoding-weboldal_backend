using System.ComponentModel.DataAnnotations;

namespace For_The_Potato_Backend.Models.Dto
{
    public class LoginDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
