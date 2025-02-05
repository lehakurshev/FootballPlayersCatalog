using MediatR;

namespace Application.Teams.Comands;

public class TryDeieteTeamCommand : IRequest
{
    public Guid TeamId  { get; set; }
}
