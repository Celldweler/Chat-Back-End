using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessagesAsync() =>
        Ok(await chatService.GetAllMessagesAsync());    
}
