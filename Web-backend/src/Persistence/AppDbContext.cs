using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
internal class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Representative> Representatives { get; set;}
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Email).HasColumnType("varchar(50)");
            entity.Property(e => e.Password).HasColumnType("varchar(100)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.HasIndex(a => a.Email).IsUnique(true);
        });

        modelBuilder.Entity<Representative>(entity =>
        {
            entity.Property(e => e.GovernmentId).HasColumnType("varchar(12)");
            entity.Property(e => e.Fullname).HasColumnType("varchar(50)");
            entity.Property(e => e.Address).HasColumnType("varchar(200)");
            entity.Property(e => e.Address).HasColumnType("varchar(200)");
            entity.Property(e => e.Gender).HasDefaultValue(true);
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.Property(e => e.Name).HasColumnType("varchar(200)");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.Email).HasColumnType("varchar(50)");
            entity.Property(e => e.Phone).HasColumnType("varchar(10)");
            entity.Property(e => e.Address).HasColumnType("varchar(200)");
            entity.Property(e => e.IsMainBranch).HasDefaultValue(false);

        });

        modelBuilder.Entity<Sector>()
            .HasKey(s => new { s.BusinessId, s.IndustryId });

        modelBuilder.Entity<Participation>()
            .HasKey(s => new { s.BusinessId, s.EventId });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Name).HasColumnType("varchar(100)");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.Property(e => e.EstablishmentCertificate).HasColumnType("varchar(50)");
            entity.Property(e => e.BusinessLicense).HasColumnType("varchar(50)");
            entity.Property(e => e.IsChecked).HasDefaultValue(false);
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasIndex(a => a.TaxCode).IsUnique(true);
            entity.Property(e => e.TaxCode).HasColumnType("varchar(15)");
            entity.Property(e => e.Name).HasColumnType("varchar(100)");
            entity.Property(e => e.WebSite).HasColumnType("varchar(50)");
            entity.Property(e => e.AvatarImage).HasColumnType("varchar(50)");
            entity.Property(e => e.CoverImage).HasColumnType("varchar(50)");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Value).HasColumnType("varchar(50)");
        });
    }
}
