using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IFootballPlayerDbContext
{
    DbSet<FootballPlayer> FootballPlayers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}