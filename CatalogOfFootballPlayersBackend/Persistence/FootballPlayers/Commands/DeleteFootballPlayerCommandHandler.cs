using Application.Common.Exceptions;
using Application.Repositories;
using Application.Teams.Comands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.DeleteFootballPlayer;

public class DeleteFootballPlayerCommandHandler : IRequestHandler<DeleteFootballPlayerCommand>
{
    private readonly IFootballPlayerRepository _footballPlayerRepository;
    
    private readonly IMediator _mediator;

    public DeleteFootballPlayerCommandHandler(IFootballPlayerRepository footballPlayerRepository, IMediator mediator)
    {
        _footballPlayerRepository = footballPlayerRepository;
        _mediator = mediator;
    }


    public async Task Handle(DeleteFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var footballPlayer =
            await _footballPlayerRepository.GetFootballPlayerByIdAsync(request.Id, cancellationToken);
        
        if (footballPlayer == null)
        {
            throw new NotFoundException();
        }

        await _footballPlayerRepository.DeleteFootballPlayerAsync(footballPlayer, cancellationToken);
        await _footballPlayerRepository.SaveChangesAsync(cancellationToken);
        
        await _mediator.Send(new TryDeieteTeamCommand{TeamId = footballPlayer.TeamId}, cancellationToken);
    }
}