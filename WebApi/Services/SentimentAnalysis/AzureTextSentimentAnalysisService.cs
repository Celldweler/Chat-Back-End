
using Azure.AI.TextAnalytics;

namespace WebApi.Services.SentimentAnalysis;

/// <summary>
/// Service for performing sentiment analysis using Azure Cognitive Services.
/// </summary>
public class AzureTextSentimentAnalysisService : ITextSentimentAnalysisService
{
    private readonly TextAnalyticsClient textAnalyticsClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureTextSentimentAnalysisService"/> class.
    /// </summary>
    /// <param name="textAnalyticsClient">
    /// An instance of <see cref="TextAnalyticsClient"/> for interacting with Azure Cognitive Services.
    /// </param>
    public AzureTextSentimentAnalysisService(TextAnalyticsClient textAnalyticsClient)
    {
        this.textAnalyticsClient = textAnalyticsClient;
    }

    /// <inheritdoc/>
    public async Task<string> AnalyzeSentimentAsync(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        var language = (await textAnalyticsClient.DetectLanguageAsync(text))?.Value.Iso6391Name
            ?? throw new ArgumentNullException();

        var sentiment = await textAnalyticsClient.AnalyzeSentimentAsync(text, language);

        return sentiment.Value.Sentiment.ToString();
    }
}
