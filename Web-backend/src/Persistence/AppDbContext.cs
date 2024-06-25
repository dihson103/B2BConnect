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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.Email)
            .IsUnique(true);

        modelBuilder.Entity<Branch>()
            .HasIndex(b => b.Email)
            .IsUnique(true);

        modelBuilder.Entity<Branch>()
            .HasIndex(b => b.Phone)
            .IsUnique(true);

        modelBuilder.Entity<Sector>()
            .HasKey(s => new { s.BusinessId, s.IndustryId });

        modelBuilder.Entity<Sector>()
            .HasOne(s => s.Business)
            .WithMany(b => b.Sectors)
            .HasForeignKey(s => s.BusinessId);

        modelBuilder.Entity<Sector>()
            .HasOne(s => s.Industry)
            .WithMany(b => b.Sectors)
            .HasForeignKey(s => s.IndustryId);

        modelBuilder.Entity<Participation>()
            .HasKey(s => new { s.BusinessId, s.EventId });

        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Business)
            .WithMany(b => b.Participations)
            .HasForeignKey(s => s.BusinessId);

        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Event)
            .WithMany(b => b.Participations)
            .HasForeignKey(s => s.EventId);
    }
}
