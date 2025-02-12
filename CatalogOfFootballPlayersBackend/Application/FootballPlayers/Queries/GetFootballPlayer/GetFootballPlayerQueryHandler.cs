using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Queries.GetFootballPlayer;

public class GetFootballPlayerQueryHandler : 
    IRequestHandler<GetFootballPlayerQuery, FootballPlayer>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    public GetFootballPlayerQueryHandler(ICatalogOfFootballPlayersDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<FootballPlayer> Handle(GetFootballPlayerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.FootballPlayers
            .FirstOrDefaultAsync(player =>
                player.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return entity;
    }
}