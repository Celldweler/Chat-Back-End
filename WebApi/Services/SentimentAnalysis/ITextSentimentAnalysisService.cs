namespace WebApi.Services.SentimentAnalysis;

public interface ITextSentimentAnalysisService
{
    /// <summary>
    /// Analyzes the sentiment of the provided text.
    /// </summary>
    /// <param name="text">The text to analyze.</param>
    /// <returns>
    /// A string representation of the sentiment analysis result. 
    /// Possible values include "Positive", "Neutral", "Negative", or "Mixed".
    /// </returns>
    /// /// <exception cref="ArgumentNullException">
    /// Thrown if the detected language is null or the language detection fails.
    /// </exception>
    Task<string> AnalyzeSentimentAsync(string text);
}
