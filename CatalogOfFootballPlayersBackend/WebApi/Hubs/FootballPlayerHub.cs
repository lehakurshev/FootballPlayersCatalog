using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace WebApi.Hubs;

[SignalRHub]
public class FootballPlayerHub : Hub
{
    public Task AddPlayer(FootballPlayer player)
    {
        return Clients.All.SendAsync("AddPlayer", player);
    }
    
    public Task UpdatePlayer(FootballPlayer player)
    {
        player.EditDate = DateTime.UtcNow;
        return Clients.All.SendAsync("UpdatePlayer", player);
    }
    
    public Task DeletePlayer(Guid playerId)
    {
        return Clients.All.SendAsync("DeletePlayer", playerId);
    }
}