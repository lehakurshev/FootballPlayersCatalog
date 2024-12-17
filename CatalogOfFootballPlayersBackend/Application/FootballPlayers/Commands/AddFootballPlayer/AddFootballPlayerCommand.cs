using MediatR;

namespace Application.FootballPlayers.Commands.AddFootballPlayer;

public class AddFootballPlayerCommand : IRequest<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Paul { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? TeamName { get; set; }
    public string? Country { get; set; }
}