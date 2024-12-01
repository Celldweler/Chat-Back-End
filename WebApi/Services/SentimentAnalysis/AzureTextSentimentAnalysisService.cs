
using Azure.AI.TextAnalytics;

namespace WebApi.Services.SentimentAnalysis;

public class AzureTextSentimentAnalysisService : ITextSentimentAnalysisService
{
    private readonly TextAnalyticsClient textAnalyticsClient;

    public AzureTextSentimentAnalysisService(TextAnalyticsClient textAnalyticsClient)
    {
        this.textAnalyticsClient = textAnalyticsClient;
    }

    public Task<string> AnalyzeSentimentAsync(string text)
    {
        throw new NotImplementedException();
    }
}
