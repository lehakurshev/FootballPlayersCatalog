using Domain;
using MediatR;

namespace Application.FootballPlayers.Queries.GetFootballPlayersList;

public class GetFootballPlayersListQuery : IRequest<IList<FootballPlayer>> { }