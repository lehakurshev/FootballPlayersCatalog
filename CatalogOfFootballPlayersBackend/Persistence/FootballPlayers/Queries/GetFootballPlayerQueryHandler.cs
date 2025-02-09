using Application.Common.Exceptions;
using Application.Repositories;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FootballPlayers.Queries.GetFootballPlayer;

public class GetFootballPlayerQueryHandler : 
    IRequestHandler<GetFootballPlayerQuery, FootballPlayer>
{
    private readonly IFootballPlayerRepository _footballPlayerRepository;
    
    public GetFootballPlayerQueryHandler(IFootballPlayerRepository footballPlayerRepository) =>
        _footballPlayerRepository = footballPlayerRepository;

    public async Task<FootballPlayer> Handle(GetFootballPlayerQuery request, CancellationToken cancellationToken)
    {
        var footballPlayer =
            await _footballPlayerRepository.GetFootballPlayerByIdAsync(request.Id, cancellationToken);

        if (footballPlayer == null)
        {
            throw new NotFoundException();
        }

        return footballPlayer;
    }
}