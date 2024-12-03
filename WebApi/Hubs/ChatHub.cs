using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Services.Chat;

namespace WebApi.Hubs;

public class ChatHub : Hub
{
    private readonly IChatService chatService;
    private readonly ILogger<ChatHub> logger;

    public ChatHub(
        IChatService chatService,
        ILogger<ChatHub> logger)
    {
        this.logger = logger;
        this.chatService = chatService;
    }

    public async Task SendMessage(string senderName, string text)
    {
        logger.LogInformation("ConnectionId: {0}", Context.ConnectionId);
        logger.LogInformation("Recieved data: {0} from user: {1}", text, senderName);

        var newMessage = await chatService.CreateMessageAsync(new CreateMessageDto(text, senderName));
        
        await Clients.All.SendAsync("ReceiveMessage", newMessage);
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
