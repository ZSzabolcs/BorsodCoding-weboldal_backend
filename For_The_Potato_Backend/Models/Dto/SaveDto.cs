using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potato_Backend.Models.Dto
{
    public class SaveDto
    {
        private string? language;

        public string? Name { get; set; }
        public int Points { get; set; }

        public int Level { get; set; }

        public string Language 
        { 
            get { return language; }
            set { if (value == "hu" || value == "en") language = value; }

        }


    }
}
