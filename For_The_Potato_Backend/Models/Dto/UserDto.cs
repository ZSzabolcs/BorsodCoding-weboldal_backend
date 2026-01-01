using System.ComponentModel.DataAnnotations;

namespace For_The_Potato_Backend.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
