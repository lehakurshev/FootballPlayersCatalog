using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace Persistence;

public sealed class FootballPlayerDbContext : DbContext, IFootballPlayerDbContext
{
    public DbSet<FootballPlayer> FootballPlayers { get; set; }
    
    public FootballPlayerDbContext(DbContextOptions<FootballPlayerDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=footballplayersdb;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FootballPlayerConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}