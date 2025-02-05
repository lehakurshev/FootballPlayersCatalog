using Application.FootballPlayers.Queries.GetFootballPlayersList;
using Application.Teams.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class TeamController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IList<Team>>> GetTeams()
    {
        var query = new GetTeamsListQuery(){};
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}

