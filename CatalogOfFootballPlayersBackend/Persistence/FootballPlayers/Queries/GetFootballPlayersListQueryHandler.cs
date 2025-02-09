
using Application.Repositories;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Queries.GetFootballPlayersList;

public class GetFootballPlayersListQueryHandler : 
    IRequestHandler<GetFootballPlayersListQuery, IList<FootballPlayer>>
{
    private readonly IFootballPlayerRepository _footballPlayerRepository;
    
    public GetFootballPlayersListQueryHandler(IFootballPlayerRepository footballPlayerRepository) =>
        _footballPlayerRepository = footballPlayerRepository;
    
    public async Task<IList<FootballPlayer>> Handle(GetFootballPlayersListQuery request, CancellationToken cancellationToken)
    {
        var footballPlayresQuery =
            await _footballPlayerRepository.GetAllFootballPlayersAsync(cancellationToken);
        
        return footballPlayresQuery;
    }
}