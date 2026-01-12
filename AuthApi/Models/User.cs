namespace AuthApi.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime RegDate { get; set; } = DateTime.Now;

        public DateTime ModDate { get; set; } = DateTime.Now;

        public string? Email { get; set; }
    }
}
