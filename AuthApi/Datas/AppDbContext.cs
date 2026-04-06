using AuthApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Datas
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; } = null!;

        public DbSet<Save> Saves { get; set; } = null!;

        public DbSet<Velemeny> Velemeny { get; set; } = null!;

        public DbSet<Mentesek> Menteseks { get; set; } = null!;

        public DbSet<Nyelvarany> Nyelvaranies { get; set; } = null!;

        public DbSet<Pontaranyegyt> Pontaranyegyts { get; set; } = null!;

        public DbSet<Szintarany> Szintaranies { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Save>(en =>
            {
                en.HasKey(e => e.Id);
                en.HasOne(s => s.User)
                .WithOne(u => u.Save)
                .HasForeignKey<Save>(s => s.Id);
            });

            builder.Entity<Velemeny>(en =>
            {
                en.HasKey(e => e.Id);
                en.HasOne(s => s.User)
                .WithOne(u => u.Velemeny)
                .HasForeignKey<Velemeny>(s => s.Id);
            });


            builder.Entity<ApplicationUser>(en =>
            {
                en.HasOne(e => e.Velemeny)
                .WithOne(s => s.User)
                .HasForeignKey<Velemeny>(s => s.Id);

                en.HasOne(e => e.Save)
                .WithOne(s => s.User)
                .HasForeignKey<Save>(s => s.Id);
            });

            builder.Entity<Mentesek>(en =>
            {
                en.HasNoKey();
                en.ToView("mentesek");
            });


            builder.Entity<Nyelvarany>(en =>
            {
                en.HasNoKey();
                en.ToView("nyelvarany");
            });

            builder.Entity<Pontaranyegyt>(en =>
            {
                en.HasNoKey();
                en.ToView("pontarany");
            });

            builder.Entity<Szintarany>(en =>
            {
                en.HasNoKey();
                en.ToView("szintarany");
            });

        }
    }
    
}
