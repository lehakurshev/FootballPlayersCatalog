using Domain;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace WebApi.Hubs;

[SignalRHub]
public class TeamHub : Hub
{
    public Task UpdateListTeams(IList<Team> listTeams)
    {
        return Clients.All.SendAsync("AddPlayer", listTeams);
    }
}