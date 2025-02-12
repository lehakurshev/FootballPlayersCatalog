﻿using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace Persistence;

public sealed class CatalogOfFootballPlayersDbContext : DbContext, ICatalogOfFootballPlayersDbContext
{
    public DbSet<FootballPlayer> FootballPlayers { get; set; }
    public DbSet<Team> Teams { get; set; }
    
    public CatalogOfFootballPlayersDbContext(DbContextOptions<CatalogOfFootballPlayersDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=footballplayersdb;Username=postgres;Password=postgres");
        
        var host       = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var port       = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var db         = Environment.GetEnvironmentVariable("DB_NAME") ?? "footballplayersdb";
        var username         = Environment.GetEnvironmentVariable("DB_USER_NAME") ?? "postgres";
        var password   = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
        var connString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";

        optionsBuilder.UseNpgsql(connString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FootballPlayerConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}