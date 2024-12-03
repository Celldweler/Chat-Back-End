namespace WebApi.Models;

public class Message
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public string TextSentiment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Sender { get; set; }
}
