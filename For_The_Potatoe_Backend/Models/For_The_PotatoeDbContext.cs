using For_The_Potatoe_Backend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;

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
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserColumns>()
                .HasIndex(u => u.Name)
                .IsUnique();

            // Konfiguráljuk az Egy-az-Egyhez kapcsolatot az User és a Save között
            modelBuilder.Entity<SaveColumns>()
                .HasKey(sc => sc.UserId);

            modelBuilder.Entity<SaveColumns>()
                .HasOne(sc => sc.UserColumns)
                .WithOne(u => u.SaveColumns)
                .HasForeignKey<SaveColumns>(sc => sc.UserId);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=for_the_potatoe;UID=root;password='';Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=Disabled");
        }



    }
}
