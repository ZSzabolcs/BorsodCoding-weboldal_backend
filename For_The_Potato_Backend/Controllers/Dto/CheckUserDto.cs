using System.ComponentModel.DataAnnotations;

namespace For_The_Potatoe_Backend.Models.Dto
{
    public class CheckUserDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
