using MediatR;

namespace Application.Teams.Queries;

public class TeamIsEmptyQuery: IRequest<Guid>
{
    public Guid TeamId  { get; set; }
}