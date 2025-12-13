using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potato_Backend.Models;

public partial class ForThePotatoContext : DbContext
{
    public ForThePotatoContext()
    {
    }

    public ForThePotatoContext(DbContextOptions<ForThePotatoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Save> Saves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=for_the_potato;user=root;password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Save>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("save");

            entity.Property(e => e.Language).HasMaxLength(2);
            entity.Property(e => e.Level).HasColumnType("int(11)");
            entity.Property(e => e.ModDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'''0001-01-01 00:00:00.000000'''");
            entity.Property(e => e.Points).HasColumnType("int(11)");
            entity.Property(e => e.RegDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'''0001-01-01 00:00:00.000000'''");

            entity.HasOne(d => d.User).WithOne(p => p.Save)
                .HasForeignKey<Save>(d => d.Id)
                .HasConstraintName("FK_Save_User_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Name, "IX_User_Name").IsUnique();

            entity.Property(e => e.Email).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'''0001-01-01 00:00:00.000000'''");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RegDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'''0001-01-01 00:00:00.000000'''");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
