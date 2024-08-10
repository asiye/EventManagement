using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EventManagement.Api.Hubs;
public class EventHub : Hub
{
    // Method to send real-time updates to clients
    public async Task SendUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveUpdate", message);
    }
}
