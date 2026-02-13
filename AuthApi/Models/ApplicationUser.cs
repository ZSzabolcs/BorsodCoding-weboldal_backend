using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public DateTime ModDate { get; set; }

        [JsonIgnore]
        public virtual Save? Save {  get; set; }
        [JsonIgnore]
        public virtual Velemeny? Velemeny { get; set; }
    }
}
