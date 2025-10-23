using System.ComponentModel.DataAnnotations.Schema;
using For_The_Potatoe_Backend.Models;
using MySql.Data.MySqlClient.X.XDevAPI.Common;

namespace For_The_Potatoe_Backend.Models
{
    public class SaveColumns
    {
        public int UserId { get; set; }

        public int Points { get; set; }

        public int Level { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string Language { get; set; }

        public DateTime Date { get; set; }

        public UserColumns UserColumns { get; set; }
    }
}
