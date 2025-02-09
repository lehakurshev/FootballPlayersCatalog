using Application.Repositories;
using Application.Teams.Comands;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.AddFootballPlayer;

public class AddFootballPlayerCommandHandler : IRequestHandler<AddFootballPlayerCommand, Guid>
{
    private readonly IFootballPlayerRepository _footballPlayerRepository;
    
    private readonly IMediator _mediator;

    public AddFootballPlayerCommandHandler(IFootballPlayerRepository footballPlayerRepository, IMediator mediator)
    {
        _footballPlayerRepository = footballPlayerRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(AddFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        
        var teamId = await _mediator.Send(new SetTeamCommand { TeamName = request.TeamName }, cancellationToken);
        
        var footballPlayer = new FootballPlayer
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth,
            Paul = request.Paul,
            TeamName = request.TeamName,
            TeamId = teamId,
            CreationDate = DateTime.UtcNow,
            EditDate = null // Если это новый игрок, то EditDate может быть null
        };
        
        await _footballPlayerRepository.AddFootballPlayerAsync(footballPlayer, cancellationToken);
        await _footballPlayerRepository.SaveChangesAsync(cancellationToken);

        return footballPlayer.Id;
    }
}