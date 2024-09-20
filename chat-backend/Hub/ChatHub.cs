using Microsoft.AspNetCore.SignalR;

namespace chat_backend.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendMessage(string msg)
    {
        await Clients.All.SendAsync("ReceiveMsg", msg);
    }
}