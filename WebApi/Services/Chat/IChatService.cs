using WebApi.DTOs;

namespace WebApi.Services.Chat;

public interface IChatService
{
    /// <summary>
    /// Get all chat messages.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> representing the result of the asynchronous operation.
    /// The task result contains a <see cref="IEnumerable{MessageDto}"/> that contains elements from the input sequence.
    /// </returns>
    Task<IEnumerable<MessageDto>> GetAllMessagesAsync();

    /// <summary>
    /// Create new ChatMessage.
    /// </summary>
    /// <param name="message">ChatMessage to create.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.
    /// The task result contains a <see cref="MessageDto"/> that was created.</returns>
    /// <exception cref="ArgumentNullException">If the parameter <see cref="CreateMessageDto"/> was not set to instance.</exception>
    Task<MessageDto> CreateMessageAsync(CreateMessageDto message);
}
