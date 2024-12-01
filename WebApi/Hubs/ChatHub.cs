using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        this.logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        logger.LogInformation("OnConnected Hook Called. ConnectionId: {0}", Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("OnDisconnected Called. ConnectionId: {0}", Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
