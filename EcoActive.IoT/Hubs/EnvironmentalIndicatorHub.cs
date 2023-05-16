using Microsoft.AspNetCore.SignalR;

namespace EcoActive.IoT.Hubs
{
    public class EnvironmentalIndicatorHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserDate", Context.ConnectionId);
        }
    }
}
