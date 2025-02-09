using Domain;

namespace Application.Repositories;

public interface ITeamRepository
{
    Task<Team> GetTeamByIdAsync(Guid teamId, CancellationToken cancellationToken);
    Task<Team?> GetTeamByNameAsync(string teamName, CancellationToken cancellationToken);
    Task<Guid> GetTeamIdByNameAsync(string teamName, CancellationToken cancellationToken);
    Task<IList<Team>> GetAllTeamsAsync(CancellationToken cancellationToken);
    
    Task AddTeamAsync(Team team, CancellationToken cancellationToken);
    Task DeleteTeam(Team team, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}