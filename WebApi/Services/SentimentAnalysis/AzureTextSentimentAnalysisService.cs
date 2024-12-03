
using Azure.AI.TextAnalytics;

namespace WebApi.Services.SentimentAnalysis;

public class AzureTextSentimentAnalysisService : ITextSentimentAnalysisService
{
    private readonly TextAnalyticsClient textAnalyticsClient;

    public AzureTextSentimentAnalysisService(TextAnalyticsClient textAnalyticsClient)
    {
        this.textAnalyticsClient = textAnalyticsClient;
    }

    public async Task<string> AnalyzeSentimentAsync(string text)
    {
        var language = (await textAnalyticsClient.DetectLanguageAsync(text))?.Value.Iso6391Name
            ?? throw new ArgumentNullException();

        var sentiment = await textAnalyticsClient.AnalyzeSentimentAsync(text, language);

        return sentiment.Value.Sentiment.ToString();
    }
}
