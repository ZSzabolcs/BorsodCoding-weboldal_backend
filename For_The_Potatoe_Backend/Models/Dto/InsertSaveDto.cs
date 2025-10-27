using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potatoe_Backend.Models.Dto
{
    public class InsertSaveDto
    {
        private string language;

        public string Name { get; set; }
        public int Points { get; set; }

        public int Level { get; set; }

        public string Language 
        { 
            get { return language; }
            set { if (value == "hu" || value == "en") language = value; }

        }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
