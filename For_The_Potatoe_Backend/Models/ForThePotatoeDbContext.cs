using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace For_The_Potatoe_Backend.Models;

public partial class ForThePotatoeDbContext : DbContext
{
    public ForThePotatoeDbContext(DbContextOptions<ForThePotatoeDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Save> Save { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=for_the_potatoe;user=root;ssl mode=none;AllowZeroDateTime=True;ConvertZeroDateTime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");


        modelBuilder.Entity<Save>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("save");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Date).HasMaxLength(6);
            entity.Property(e => e.Language).HasMaxLength(2);
            entity.Property(e => e.Level).HasColumnType("int(11)");
            entity.Property(e => e.Points).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithOne(p => p.Save)
                .HasForeignKey<Save>(d => d.UserId)
                .HasConstraintName("FK_Save_User_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Name, "IX_User_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Date).HasMaxLength(6);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
