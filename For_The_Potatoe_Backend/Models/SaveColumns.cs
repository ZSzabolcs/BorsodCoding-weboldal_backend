using System.ComponentModel.DataAnnotations.Schema;
using For_The_Potatoe_Backend.Models;

namespace For_The_Potatoe_Backend.Models
{
    public class SaveColumns
    {
        public int Id { get; set; }

        public int Points { get; set; }

        public int Level { get; set; }

        public string Language { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public UserColumns UserColumns { get; set; }
    }
}
