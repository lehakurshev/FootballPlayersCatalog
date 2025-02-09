using Application.Repositories;
using Application.Teams.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Teams.Comands;

public class SetTeamCommandHandler  : IRequestHandler<SetTeamCommand, Guid>
{
    private readonly ITeamRepository _teamRepository;


    public SetTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Guid> Handle(SetTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = await _teamRepository.GetTeamIdByNameAsync(request.TeamName, cancellationToken);
        
        if (teamId != Guid.Empty)
        {
            return teamId;
        }
        else
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = request.TeamName,
            };

            await _teamRepository.AddTeamAsync(team, cancellationToken);
            await _teamRepository.SaveChangesAsync(cancellationToken);
        
            return team.Id;
        }
    }
}