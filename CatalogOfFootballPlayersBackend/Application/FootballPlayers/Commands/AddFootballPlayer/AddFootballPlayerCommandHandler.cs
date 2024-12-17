using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.AddFootballPlayer;

public class AddFootballPlayerCommandHandler : IRequestHandler<AddFootballPlayerCommand, Guid>
{
    private readonly IFootballPlayerDbContext _dbContext;

    public AddFootballPlayerCommandHandler(IFootballPlayerDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(AddFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var footballPlayer = new FootballPlayer
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth,
            Paul = request.Paul,
            TeamName = request.TeamName,
            CreationDate = DateTime.UtcNow,
            EditDate = null // Если это новый игрок, то EditDate может быть null
        };
        
        await _dbContext.FootballPlayers.AddAsync(footballPlayer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return footballPlayer.Id;
    }
}