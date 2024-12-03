using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Services.SentimentAnalysis;

namespace WebApi.Services.Chat;

public class ChatService : IChatService
{
    private readonly IMapper mapper;
    private readonly ILogger<ChatService> logger;
    private readonly ApplicationDataContext context;
    private readonly ITextSentimentAnalysisService sentimentAnalysisService;

    public ChatService(
        IMapper mapper,
        ILogger<ChatService> logger,
        ApplicationDataContext context,
        ITextSentimentAnalysisService sentimentAnalysisService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.context = context;
        this.sentimentAnalysisService = sentimentAnalysisService;
    }

    /// <inheritdoc/>
    public async Task<MessageDto> CreateMessageAsync(CreateMessageDto message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));
        if (string.IsNullOrEmpty(message.Text) || string.IsNullOrEmpty(message.Sender))
        {
            throw new ArgumentNullException();
        }

        var newMessage = mapper.Map<Message>(message);
        newMessage.TextSentiment = await sentimentAnalysisService.AnalyzeSentimentAsync(message.Text);

        await context.AddAsync(newMessage);
        await context.SaveChangesAsync();

        return mapper.Map<MessageDto>(newMessage);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
    {
        var messages = await context.Messages.ToListAsync();

        return mapper.Map<IEnumerable<MessageDto>>(messages);
    }
}
