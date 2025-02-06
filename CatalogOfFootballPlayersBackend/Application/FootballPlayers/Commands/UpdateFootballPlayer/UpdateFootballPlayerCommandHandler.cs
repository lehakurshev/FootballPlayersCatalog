using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Teams.Comands;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.UpdateFootballPlayer;

public class UpdateFootballPlayerCommandHandler : IRequestHandler<UpdateFootballPlayerCommand>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    private readonly IMediator _mediator;

    public UpdateFootballPlayerCommandHandler(ICatalogOfFootballPlayersDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task Handle(UpdateFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.FootballPlayers.FirstOrDefaultAsync(player =>
                player.Id == request.Id, cancellationToken);
        
        var oldTeamId = entity.TeamId;

        if (entity == null)
        {
            throw new NotFoundException();
        }
        
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Country = request.Country;
        entity.TeamName = request.TeamName;
        entity.TeamId = await _mediator.Send(new SetTeamCommand{TeamName = request.TeamName}, cancellationToken);
        entity.Paul = request.Paul;
        entity.EditDate = DateTime.UtcNow;
        entity.DateOfBirth = request.DateOfBirth;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (oldTeamId != entity.TeamId)
        {
            await _mediator.Send(new TryDeieteTeamCommand{TeamId = oldTeamId}, cancellationToken);
        }
    }
}