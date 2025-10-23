using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potatoe_Backend.Models.Dto
{
    public class InsertSaveDto
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public int Level { get; set; }

        public string Language { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
