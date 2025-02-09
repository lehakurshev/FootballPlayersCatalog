using Domain;

namespace Application.Repositories;

public interface IFootballPlayerRepository
{
    Task<FootballPlayer> GetFootballPlayerByIdAsync(Guid playerId, CancellationToken cancellationToken);
    Task<IList<FootballPlayer>> GetAllFootballPlayersAsync(CancellationToken cancellationToken);
    Task<Guid> AddFootballPlayerAsync(FootballPlayer player, CancellationToken cancellationToken);
    Task DeleteFootballPlayerAsync(FootballPlayer player, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    Task<FootballPlayer> GetFirstFootballPlayerByTeamIdAsync(Guid teamId, CancellationToken cancellationToken);
    
}