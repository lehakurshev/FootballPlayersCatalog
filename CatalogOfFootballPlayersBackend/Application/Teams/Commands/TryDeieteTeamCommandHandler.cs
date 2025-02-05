using Application.Interfaces;
using Application.Teams.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Comands;

public class TryDeieteTeamCommandHandler : IRequestHandler<TryDeieteTeamCommand>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    private readonly IMediator _mediator;

    public TryDeieteTeamCommandHandler(ICatalogOfFootballPlayersDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task Handle(TryDeieteTeamCommand request, CancellationToken cancellationToken)
    {
        var teamIsEmptyQuery = new TeamIsEmptyQuery
        {
            TeamId = request.TeamId
        };
        
        var teamIsEmpty = await _mediator.Send(teamIsEmptyQuery, cancellationToken);

        if (teamIsEmpty == Guid.Empty)
        {
            await _dbContext.Teams.FirstOrDefaultAsync(team =>
                team.Id == request.TeamId, cancellationToken);
        }
    }
}