using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Queries.GetFootballPlayersList;

public class GetFootballPlayersListQueryHandler : 
    IRequestHandler<GetFootballPlayersListQuery, List<FootballPlayer>>
{
    private readonly IFootballPlayerDbContext _dbContext;
    
    public GetFootballPlayersListQueryHandler(IFootballPlayerDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task<List<FootballPlayer>> Handle(GetFootballPlayersListQuery request, CancellationToken cancellationToken)
    {
        var footballPlayresQuery = await _dbContext.FootballPlayers
            .ToListAsync(cancellationToken);
        
        return footballPlayresQuery;
    }
}