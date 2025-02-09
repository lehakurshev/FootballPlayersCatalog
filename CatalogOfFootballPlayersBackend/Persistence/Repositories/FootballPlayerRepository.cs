using Application.Common.Exceptions;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FootballPlayerRepository : IFootballPlayerRepository
{
    private readonly CatalogOfFootballPlayersDbContext _dbContext;
    
    FootballPlayerRepository(CatalogOfFootballPlayersDbContext dbContext) => _dbContext = dbContext;

    public async Task<FootballPlayer> GetFootballPlayerByIdAsync(Guid playerId, CancellationToken cancellationToken)
    {
        var footballPlayer = await _dbContext.FootballPlayers
            .FirstOrDefaultAsync(player =>
                player.Id == playerId, cancellationToken);
        if (footballPlayer == null)
        {
            throw new NotFoundException();
        }
        return footballPlayer;
    }

    public async Task<IList<FootballPlayer>> GetAllFootballPlayersAsync(CancellationToken cancellationToken)
    {
        var footballPlayresQuery = await _dbContext.FootballPlayers.ToListAsync(cancellationToken);
        return footballPlayresQuery;
    }

    public async Task<Guid> AddFootballPlayerAsync(FootballPlayer footballPlayer, CancellationToken cancellationToken)
    {
        await _dbContext.FootballPlayers.AddAsync(footballPlayer, cancellationToken);
        return footballPlayer.Id;
    }


    public Task DeleteFootballPlayerAsync(FootballPlayer player, CancellationToken cancellationToken)
    {
        _dbContext.FootballPlayers.Remove(player);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<FootballPlayer> GetFirstFootballPlayerByTeamIdAsync(Guid teamId, CancellationToken cancellationToken)
    {
        var players = await _dbContext.FootballPlayers
            .Where(fp => fp.TeamId == teamId)
            .FirstOrDefaultAsync(cancellationToken);
        return players;
    }
}