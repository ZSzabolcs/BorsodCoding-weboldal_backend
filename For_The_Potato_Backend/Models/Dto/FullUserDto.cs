using System.ComponentModel.DataAnnotations;

namespace For_The_Potato_Backend.Models.Dto
{
    public class FullUserDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime RegDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModDate { get; set; } = DateTime.Now;
    }
}
