using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.Services.Chat;

public class ChatService : IChatService
{
    private readonly IMapper mapper;
    private readonly ILogger<ChatService> logger;
    private readonly ApplicationDataContext context;

    public ChatService(
        IMapper mapper,
        ILogger<ChatService> logger,
        ApplicationDataContext context)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.context = context;
    }

    public async Task CreateMessageAsync(CreateMessageDto message)
    {
        var newMessage = mapper.Map<Message>(message);
        newMessage.TextSentyment = "stub";

        await context.AddAsync(newMessage);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
    {
        var messages = await context.Messages.ToListAsync();

        return mapper.Map<IEnumerable<MessageDto>>(messages);
    }
}
