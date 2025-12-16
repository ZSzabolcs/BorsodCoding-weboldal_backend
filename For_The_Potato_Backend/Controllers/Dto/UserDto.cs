using System.ComponentModel.DataAnnotations;

namespace For_The_Potatoe_Backend.Models.Dto
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
