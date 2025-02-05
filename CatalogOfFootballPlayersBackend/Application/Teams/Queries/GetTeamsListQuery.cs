using Domain;
using MediatR;

namespace Application.Teams.Queries;

public class GetTeamsListQuery : IRequest<IList<Team>> { }