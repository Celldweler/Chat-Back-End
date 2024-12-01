namespace WebApi.DTOs;

public class MessageDto
{
    public int Id { get; set; }

    public string Text { get; set; }

    public string TextSentyment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Sender { get; set; }
}
