using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using For_The_Potatoe_Backend.Models;

namespace For_The_Potatoe_Backend.Models
{
    public class UserColumns
    {
        [Key]
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }

        public ICollection<SaveColumns> Saves { get; }
    }
}
