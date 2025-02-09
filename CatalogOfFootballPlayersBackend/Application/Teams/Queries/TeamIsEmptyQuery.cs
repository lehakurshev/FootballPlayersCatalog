using MediatR;

namespace Application.Teams.Queries;

public class TeamIsEmptyQuery: IRequest<bool>
{
    public Guid TeamId  { get; set; }
}