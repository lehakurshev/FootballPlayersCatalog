using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface ICatalogOfFootballPlayersDbContext
{
    DbSet<FootballPlayer> FootballPlayers { get; set; }
    DbSet<Team> Teams { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}