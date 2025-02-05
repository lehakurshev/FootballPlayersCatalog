using Application.FootballPlayers.Queries.GetFootballPlayersList;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Queries;

public class GetTeamsListQueryHandler : IRequestHandler<GetTeamsListQuery, IList<Team>>
{
    private readonly ICatalogOfFootballPlayersDbContext _dbContext;

    public GetTeamsListQueryHandler(ICatalogOfFootballPlayersDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<IList<Team>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
    {
        var teams = await _dbContext.Teams.ToListAsync(cancellationToken);
        return teams;
    }
}