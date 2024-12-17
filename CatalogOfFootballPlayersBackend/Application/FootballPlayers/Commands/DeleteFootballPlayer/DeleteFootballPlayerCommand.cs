using MediatR;

namespace Application.FootballPlayers.Commands.DeleteFootballPlayer;

public class DeleteFootballPlayerCommand : IRequest
{
    public Guid Id { get; set; }
}