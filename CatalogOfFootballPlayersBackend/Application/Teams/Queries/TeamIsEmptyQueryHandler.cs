using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Queries;

public class TeamIsEmptyQueryHandler : IRequestHandler<TeamIsEmptyQuery, Guid>
{
    
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;

    public TeamIsEmptyQueryHandler(ICatalogOfFootballPlayersDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task<Guid> Handle(TeamIsEmptyQuery request, CancellationToken cancellationToken)
    {
        var existingTeamId = await _dbContext.FootballPlayers
            .Where(fp => fp.TeamId == request.TeamId)
            .Select(fp => fp.TeamId)
            .FirstOrDefaultAsync(cancellationToken);

        return existingTeamId;
    }
}