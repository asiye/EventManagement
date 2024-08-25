using Microsoft.AspNetCore.SignalR;
using Nethereum.JsonRpc.Client;

namespace EventManagement.Api.Hubs;
public class BlockchainHub : Hub
{
    public async Task SendNewBlock(string blockHash)
    {
        await Clients.All.SendAsync("ReceiveBlock", blockHash);
    }

    public async Task SendNewTransaction(string txHash)
    {
        await Clients.All.SendAsync("ReceiveTransaction", txHash);
    }
}

