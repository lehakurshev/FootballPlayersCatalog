using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Teams.Comands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.DeleteFootballPlayer;

public class DeleteFootballPlayerCommandHandler : IRequestHandler<DeleteFootballPlayerCommand>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    private readonly IMediator _mediator;

    public DeleteFootballPlayerCommandHandler(ICatalogOfFootballPlayersDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }


    public async Task Handle(DeleteFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.FootballPlayers.FirstOrDefaultAsync(player =>
            player.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException();
        }

        _dbContext.FootballPlayers.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _mediator.Send(new TryDeieteTeamCommand{TeamId = entity.TeamId}, cancellationToken);
    }
}