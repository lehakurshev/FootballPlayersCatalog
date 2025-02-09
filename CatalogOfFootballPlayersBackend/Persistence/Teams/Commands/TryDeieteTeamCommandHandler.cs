using Application.Repositories;
using Application.Teams.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Comands;

public class TryDeieteTeamCommandHandler : IRequestHandler<TryDeieteTeamCommand>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMediator _mediator;

    public TryDeieteTeamCommandHandler(ITeamRepository teamRepository, IMediator mediator)
    {
        ITeamRepository _teamRepository = teamRepository;
        _mediator = mediator;
    }

    public async Task Handle(TryDeieteTeamCommand request, CancellationToken cancellationToken)
    {
        var teamIsEmptyQuery = new TeamIsEmptyQuery { TeamId = request.TeamId };
        var teamIsEmpty = await _mediator.Send(teamIsEmptyQuery, cancellationToken); // надо будет изменить

        if (teamIsEmpty)
        {

            var team = await _teamRepository.GetTeamByIdAsync(request.TeamId, cancellationToken);
            await _teamRepository.DeleteTeam(team, cancellationToken);
            await _teamRepository.SaveChangesAsync(cancellationToken);
        }
    }
}