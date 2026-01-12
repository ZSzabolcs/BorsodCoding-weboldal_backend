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

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Mentesek> Menteseks { get; set; }

    public virtual DbSet<Nyelvarany> Nyelvaranies { get; set; }

    public virtual DbSet<Pontaranyegyt> Pontaranyegyts { get; set; }

    public virtual DbSet<Pontok> Pontoks { get; set; }

    public virtual DbSet<Pontok2> Pontok2s { get; set; }

    public virtual DbSet<Save> Saves { get; set; }

    public virtual DbSet<Szintarany> Szintaranies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=for_the_potato;user=root;password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.ConcurrencyStamp).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.NormalizedName)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ClaimType).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ClaimValue).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");
            entity.Property(e => e.Birthdate).HasMaxLength(6);
            entity.Property(e => e.ConcurrencyStamp).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FullName).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LockoutEnd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.ModDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'''0001-01-01 00:00:00.000000'''");
            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PasswordHash).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PhoneNumber).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SecurityStamp).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .HasDefaultValueSql("'NULL'");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ClaimType).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ClaimValue).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PRIMARY");

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.ProviderDisplayName).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.ToTable("aspnetusertokens");

            entity.Property(e => e.Value).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Mentesek>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("mentesek");

            entity.Property(e => e.Db)
                .HasColumnType("bigint(21)")
                .HasColumnName("DB");
        });

        modelBuilder.Entity<Nyelvarany>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("nyelvarany");

            entity.Property(e => e.Language).HasMaxLength(2);
            entity.Property(e => e.Szazalek)
                .HasPrecision(10, 1)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Pontaranyegyt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pontaranyegyt");

            entity.Property(e => e.Points).HasColumnType("int(11)");
            entity.Property(e => e.Szazalek)
                .HasPrecision(10, 1)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Pontok>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pontok");

            entity.Property(e => e.Points).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Pontok2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pontok2");

            entity.Property(e => e.Points).HasColumnType("int(11)");
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

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Save)
                .HasForeignKey<Save>(d => d.Id)
                .HasConstraintName("save_ibfk_1");
        });

        modelBuilder.Entity<Szintarany>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("szintarany");

            entity.Property(e => e.Level).HasColumnType("int(11)");
            entity.Property(e => e.Szazalek)
                .HasPrecision(10, 1)
                .HasDefaultValueSql("'NULL'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
