using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Queries;

public class TeamIsEmptyQueryHandler : IRequestHandler<TeamIsEmptyQuery, bool>
{
    
    private readonly IFootballPlayerRepository _footballPlayerRepository;

    public TeamIsEmptyQueryHandler(IFootballPlayerRepository footballPlayerRepository) =>
        _footballPlayerRepository = footballPlayerRepository;
    
    public async Task<bool> Handle(TeamIsEmptyQuery request, CancellationToken cancellationToken)
    {
        var player = await _footballPlayerRepository
            .GetFirstFootballPlayerByTeamIdAsync(request.TeamId, cancellationToken);

        return player == null;
    }
}