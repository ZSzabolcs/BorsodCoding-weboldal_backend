using System.ComponentModel.DataAnnotations;

namespace For_The_Potato_Backend.Models.Dto
{
    public class SaveDtoFromWPF
    {
        private string? language;

        [Required]
        public string Id { get; set; }
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