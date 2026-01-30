using Microsoft.AspNetCore.Identity;

namespace AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public DateTime ModDate { get; set; }
    }
}
