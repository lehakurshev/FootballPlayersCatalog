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
        //optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Database=footballplayersdb;Username=postgres;Password=postgres");
        
        var host       = "rc1a-d12zl0ys84ef6bd6.mdb.yandexcloud.net";
        var port       = "6432";
        var db         = "db1";
        var username   = "user1";
        var password   = "nfyrb2017";
        var connString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";

        optionsBuilder.UseNpgsql(connString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FootballPlayerConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}