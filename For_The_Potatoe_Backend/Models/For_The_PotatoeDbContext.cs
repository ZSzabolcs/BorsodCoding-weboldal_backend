using Microsoft.EntityFrameworkCore;

namespace For_The_Potatoe_Backend.Models
{
    public class For_The_PotatoeDbContext : DbContext
    {
        public For_The_PotatoeDbContext()
        {

        }

        public For_The_PotatoeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserColumns> User {  get; set; }

        public DbSet<SaveColumns> Save { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserColumns>()
                .HasMany(e => e.Saves)
                .WithOne(e => e.UserColumns)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=for_the_potatoe;UID=root;password='';SslMode=None");
        }

    }
}
