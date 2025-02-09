using Application.FootballPlayers.Queries.GetFootballPlayersList;
using Application.Repositories;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Queries;

public class GetTeamsListQueryHandler : IRequestHandler<GetTeamsListQuery, IList<Team>>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamsListQueryHandler(ITeamRepository teamRepository) =>
        _teamRepository = teamRepository;

    public async Task<IList<Team>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.GetAllTeamsAsync(cancellationToken);
        return teams;
    }
}