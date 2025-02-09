using Application.Common.Exceptions;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly CatalogOfFootballPlayersDbContext _dbContext;
    
    TeamRepository(CatalogOfFootballPlayersDbContext dbContext) => _dbContext = dbContext;

    public async Task<Team> GetTeamByIdAsync(Guid teamId, CancellationToken cancellationToken)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(team =>
            team.Id == teamId, cancellationToken);
        
        if (team == null)
        {
            throw new NotFoundException();
        }
        
        return team;
    }

    public async Task<Team?> GetTeamByNameAsync(string teamName, CancellationToken cancellationToken)
    {
        var team = await _dbContext.Teams
            .Where(fp => fp.Name == teamName)
            .FirstOrDefaultAsync(cancellationToken);


        return team;
    }
    
    public async Task<Guid> GetTeamIdByNameAsync(string teamName, CancellationToken cancellationToken)
    {
        var team = await GetTeamByNameAsync(teamName, cancellationToken);
        
        if (team == null)
        {
            return Guid.Empty;
        }
        
        return team.Id;
    }

    public async Task<IList<Team>> GetAllTeamsAsync(CancellationToken cancellationToken)
    {
        var teams = await _dbContext.Teams.ToListAsync(cancellationToken);
        return teams;
    }

    

    public async Task AddTeamAsync(Team team, CancellationToken cancellationToken)
    {
        await _dbContext.Teams.AddAsync(team, cancellationToken);
    }

    public Task DeleteTeam(Team team, CancellationToken cancellationToken)
    {
        _dbContext.Teams.Remove(team);
        return Task.CompletedTask;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}