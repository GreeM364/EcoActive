using Microsoft.AspNetCore.SignalR;

namespace EcoActive.IoT.Hubs
{
    public class EnvironmentalIndicatorHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserDate", Context.ConnectionId);
        }

        public async Task Enter(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
