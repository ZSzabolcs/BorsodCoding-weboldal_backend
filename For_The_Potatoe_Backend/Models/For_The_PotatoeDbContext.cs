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

        /*
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<For_The_PotatoeDbContext>();

                await context.Database.MigrateAsync();

                if (!context.User.Any()) // ELLENŐRZÉS
                {
                    using (StreamReader sr = new StreamReader("UserData.csv"))
                    {
                        while (!sr.EndOfStream)
                        {
                            var sor = sr.ReadLine().Split(';');
                            context.User.Add(new UserColumns
                            {
                                Id = int.Parse(sor[0]),
                                Name = sor[1],
                                Password = sor[2],
                                Date = DateTime.Parse(sor[3])
                            });
                        }

                    }

                    using (StreamReader sr = new StreamReader("SaveData.csv"))
                    {
                        while (!sr.EndOfStream)
                        {
                            var sor = sr.ReadLine().Split(',');
                            context.Save.Add(new SaveColumns
                            {
                                Id = int.Parse(sor[0]),
                                Points = int.Parse(sor[1]),
                                Level = int.Parse(sor[2]),
                                Language = sor[3],
                                Date = DateTime.Parse(sor[4]),
                                UserId = int.Parse(sor[0])
                            });
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        
        }
        */

  
        

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
