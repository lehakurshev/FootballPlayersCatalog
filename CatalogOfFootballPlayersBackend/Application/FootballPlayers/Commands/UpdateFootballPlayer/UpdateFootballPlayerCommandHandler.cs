using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.UpdateFootballPlayer;

public class UpdateFootballPlayerCommandHandler : IRequestHandler<UpdateFootballPlayerCommand>
{
    private readonly IFootballPlayerDbContext _dbContext;

    public UpdateFootballPlayerCommandHandler(IFootballPlayerDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task Handle(UpdateFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.FootballPlayers.FirstOrDefaultAsync(note =>
                note.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Country = request.Country;
        entity.TeamName = request.TeamName;
        entity.Paul = request.Paul;
        entity.EditDate = DateTime.UtcNow;
        entity.DateOfBirth = request.DateOfBirth;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}