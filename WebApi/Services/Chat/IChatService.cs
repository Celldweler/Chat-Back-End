using WebApi.DTOs;

namespace WebApi.Services.Chat;

public interface IChatService
{
    Task<IEnumerable<MessageDto>> GetAllMessagesAsync();

    Task CreateMessageAsync(CreateMessageDto message);
}
