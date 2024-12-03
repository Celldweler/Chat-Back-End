using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Services.Chat;

namespace WebApi.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatService chatService;

    public ChatController(IChatService chatService)
    {
        this.chatService = chatService;
    }

    /// <summary>
    /// Gets the all messages.
    /// </summary>
    /// <returns>The messages.</returns>
    [AllowAnonymous]
    [HttpGet("messages")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MessageDto>))]
    public async Task<IActionResult> GetMessagesAsync() =>
        Ok(await chatService.GetAllMessagesAsync());    
}
