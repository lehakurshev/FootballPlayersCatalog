using Application.Common.Exceptions;
using Application.Repositories;
using Application.Teams.Comands;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.UpdateFootballPlayer;

public class UpdateFootballPlayerCommandHandler : IRequestHandler<UpdateFootballPlayerCommand>
{
    private readonly IFootballPlayerRepository _footballPlayerRepository;
    
    private readonly IMediator _mediator;

    public UpdateFootballPlayerCommandHandler(IFootballPlayerRepository footballPlayerRepository, IMediator mediator)
    {
        _footballPlayerRepository = footballPlayerRepository;
        _mediator = mediator;
    }

    public async Task Handle(UpdateFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var footballPlayer =
            await _footballPlayerRepository.GetFootballPlayerByIdAsync(request.Id, cancellationToken);
        
        var oldTeamId = footballPlayer.TeamId;

        if (footballPlayer == null)
        {
            throw new NotFoundException();
        }
        
        footballPlayer.FirstName = request.FirstName;
        footballPlayer.LastName = request.LastName;
        footballPlayer.Country = request.Country;
        footballPlayer.TeamName = request.TeamName;
        footballPlayer.TeamId = await _mediator.Send(new SetTeamCommand{TeamName = request.TeamName}, cancellationToken);
        footballPlayer.Paul = request.Paul;
        footballPlayer.EditDate = DateTime.UtcNow;
        footballPlayer.DateOfBirth = request.DateOfBirth;
        
        await _footballPlayerRepository.SaveChangesAsync(cancellationToken);

        if (oldTeamId != footballPlayer.TeamId)
        {
            await _mediator.Send(new TryDeieteTeamCommand{TeamId = oldTeamId}, cancellationToken);
        }
    }
}