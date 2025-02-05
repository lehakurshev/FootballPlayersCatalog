using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Queries.GetFootballPlayersList;

public class GetFootballPlayersListQueryHandler : 
    IRequestHandler<GetFootballPlayersListQuery, IList<FootballPlayer>>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;
    
    public GetFootballPlayersListQueryHandler(ICatalogOfFootballPlayersDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task<IList<FootballPlayer>> Handle(GetFootballPlayersListQuery request, CancellationToken cancellationToken)
    {
        var footballPlayresQuery = await _dbContext.FootballPlayers.ToListAsync(cancellationToken);
        
        return footballPlayresQuery;
    }
}