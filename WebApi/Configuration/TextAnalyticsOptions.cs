namespace WebApi.Configuration;

public class TextAnalyticsOptions
{
    public const string Section = "AzureAITextAnalytics";

    public string Endpoint { get; set; }

    public string Key { get; set; }
}
