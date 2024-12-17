using Application.FootballPlayers.Commands.AddFootballPlayer;
using Application.FootballPlayers.Commands.DeleteFootballPlayer;
using Application.FootballPlayers.Commands.UpdateFootballPlayer;
using Application.FootballPlayers.Queries.GetFootballPlayersList;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class FootballPlayerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<FootballPlayer>>> GetAll()
    {
        var query = new GetFootballPlayersListQuery();
        return Ok(query);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add([FromBody] AddFootballPlayerDto player)
    {
        var command = new AddFootballPlayerCommand
        {
            FirstName = player.FirstName,
            LastName = player.LastName,
            TeamName = player.TeamName,
            Country = player.Country,
            DateOfBirth = player.DateOfBirth,
            Paul = player.Paul
        };
        var noteId = await Mediator.Send(command);
        return Ok(noteId);
    }
    
    [HttpPut]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateFootballPlayerDto player)
    {
        var command = new UpdateFootballPlayerCommand
        {
            Id = player.Id,
            FirstName = player.FirstName,
            LastName = player.LastName,
            TeamName = player.TeamName,
            Country = player.Country,
            DateOfBirth = player.DateOfBirth,
            Paul = player.Paul
        };
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteFootballPlayerCommand
        {
            Id = id,
        };
        await Mediator.Send(command);
        return NoContent();
    }
}