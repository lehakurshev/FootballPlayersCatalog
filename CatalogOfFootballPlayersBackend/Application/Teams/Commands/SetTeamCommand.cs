using MediatR;

namespace Application.Teams.Comands;

public class SetTeamCommand : IRequest<Guid>
{
    public string? TeamName { get; set; }
}