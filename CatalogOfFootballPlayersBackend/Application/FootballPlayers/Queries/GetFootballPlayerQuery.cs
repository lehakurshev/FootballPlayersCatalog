using Domain;
using MediatR;

namespace Application.FootballPlayers.Queries.GetFootballPlayer;

public class GetFootballPlayerQuery : IRequest<FootballPlayer>
{
    public Guid Id { get; set; }
}
