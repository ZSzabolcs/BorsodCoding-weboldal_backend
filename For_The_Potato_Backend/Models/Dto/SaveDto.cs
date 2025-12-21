using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potato_Backend.Models.Dto
{
    public class SaveDto
    {
        private string? language;

        [Required]
        public string? Name { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string Language 
        { 
            get { return language; }
            set { if (value == "hu" || value == "en") language = value; }

        }



    }
}
