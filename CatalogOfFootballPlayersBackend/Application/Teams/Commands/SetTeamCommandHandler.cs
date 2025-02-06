using Application.Interfaces;
using Application.Teams.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Comands;

public class SetTeamCommandHandler  : IRequestHandler<SetTeamCommand, Guid>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    private readonly IMediator _mediator;

    public SetTeamCommandHandler(ICatalogOfFootballPlayersDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(SetTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = await _dbContext.Teams
            .Where(fp => fp.Name == request.TeamName)
            .Select(fp => fp.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (teamId != Guid.Empty)
        {
            return teamId;
        }
        else
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = request.TeamName,
            };

            await _dbContext.Teams.AddAsync(team, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return team.Id;
        }
    }
}