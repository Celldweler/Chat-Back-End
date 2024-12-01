namespace WebApi.DTOs;

public class CreateMessageDto
{
    public string Text { get; set; } = string.Empty;

    public string Sender { get; set; }
}
