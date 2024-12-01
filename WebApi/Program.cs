using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Configuration;
using WebApi.Data;
using WebApi.Hubs;
using WebApi.Services.Chat;
using WebApi.Services.SentimentAnalysis;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR()
    .AddAzureSignalR();
builder.Services.AddDbContext<ApplicationDataContext>(options =>
{
    options.UseSqlServer(
        configuration.GetConnectionString(
            environment.IsDevelopment() ? "SqlServer" : "AzureSQL"));    
});
builder.Services.Configure<TextAnalyticsOptions>(configuration.GetSection(TextAnalyticsOptions.Section));
builder.Services.AddSingleton(provider =>
{
    return new TextAnalyticsClient(
        new Uri(configuration["AzureAITextAnalytics:Endpoint"]!),
        new AzureKeyCredential(configuration["AzureAITextAnalytics:Key"]!)
    );
});

builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<ITextSentimentAnalysisService, AzureTextSentimentAnalysisService>();

builder.Services.AddCors(confg =>
    confg.AddPolicy(
        AppConstants.CorsPolicy,
        p => p.WithOrigins(configuration["AllowedCorsOrigins"]!.Split(','))
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(AppConstants.CorsPolicy);

app.MapControllers();
app.MapHub<ChatHub>(AppConstants.PathToChatHub);

ApplyDbMigrations(app.Services);

app.Run();


void ApplyDbMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Applying database migrations...");
        ctx.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogInformation($"Migration failed: {ex.Message}");
        throw;
    }
}
