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
        var teamIsEmptyQuery = new TeamIsEmptyQuery
        {
            TeamId = request.TeamId
        };
        
        var teamIsEmpty = await _mediator.Send(teamIsEmptyQuery, cancellationToken);
        
        if (teamIsEmpty != Guid.Empty)
        {
            return teamIsEmpty;
        }
        else
        {
            var team = new Team
            {
                Id = request.TeamId == Guid.Empty ? Guid.NewGuid() : request.TeamId, // это надо будет поменять
                Name = request.TeamName,
            };

            await _dbContext.Teams.AddAsync(team, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return team.Id;
        }
    }
}