using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class FootballPlayerHub : Hub
{
    public Task AddPlayer(FootballPlayer player)
    {
        return Clients.All.SendAsync("AddPlayer", player);
    }
    
    public Task DeletePlayer(Guid playerId)
    {
        return Clients.All.SendAsync(nameof(DeletePlayer), playerId);
    }
}