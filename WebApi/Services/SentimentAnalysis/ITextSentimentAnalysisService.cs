namespace WebApi.Services.SentimentAnalysis;

public interface ITextSentimentAnalysisService
{
    /// <summary>
    /// Runs a predictive model to identify the positive, negative, neutral
    /// or mixed sentiment contained in the text.
    /// </summary>
    /// <param name="text">The text to analyze.</param>
    /// <returns>The predicted sentiment for a given text.</returns>
    Task<string> AnalyzeSentimentAsync(string text);
}
