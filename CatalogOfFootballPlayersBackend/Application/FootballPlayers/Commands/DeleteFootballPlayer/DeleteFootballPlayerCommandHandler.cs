using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Commands.DeleteFootballPlayer;

public class DeleteFootballPlayerCommandHandler : IRequestHandler<DeleteFootballPlayerCommand>
{
    private readonly IFootballPlayerDbContext _dbContext;

    public DeleteFootballPlayerCommandHandler(IFootballPlayerDbContext dbContext) =>
        _dbContext = dbContext;


    public async Task Handle(DeleteFootballPlayerCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.FootballPlayers.FirstOrDefaultAsync(note =>
            note.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException();
        }

        _dbContext.FootballPlayers.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}