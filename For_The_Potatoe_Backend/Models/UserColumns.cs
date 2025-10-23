using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using For_The_Potatoe_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potatoe_Backend.Models
{
    public class UserColumns
    {
        [Key]
        public  int Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        public DateTime Date { get; set; }

        public SaveColumns SaveColumns { get; }
    }
}
