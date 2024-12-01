using WebApi.Data;
using WebApi.DTOs;

namespace WebApi.Services.Chat;

public class ChatService : IChatService
{
    private readonly ILogger<ChatService> logger;
    private readonly ApplicationDataContext context;

    public ChatService(
        ILogger<ChatService> logger,
        ApplicationDataContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public Task CreateMessageAsync(CreateMessageDto message)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
    {
        throw new NotImplementedException();
    }
}
