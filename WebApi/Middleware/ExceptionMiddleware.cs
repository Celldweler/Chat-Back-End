using System.Text.Json;

namespace WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddlewareExtension"/> class.
    /// </summary>
    /// <param name="next">Next delegate.</param>
    /// <param name="logger">Logger.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Request data is null");
            var messageForUser = "Request data is empty. Please check your input data and try again.";
            await HandleExceptionAsync(context, messageForUser, StatusCodes.Status400BadRequest);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError($"Exception information: {ex}");

            var messageForUser = "Validation error. Please check your input data and try again. If you are sure of input data please contact support.";

            await HandleExceptionAsync(context, messageForUser, StatusCodes.Status400BadRequest);
        }
        catch(Azure.RequestFailedException ex)
        {
            _logger.LogError($"Azure Request Failed: {ex.Message}, Status: {ex.Status}, Error Code: {ex.ErrorCode}");

            var messageForUser = ex.Status switch
            {
                401 => "Unauthorized access to Azure service. Please check your credentials.",
                403 => "Forbidden access to Azure service. Please verify your permissions.",
                429 => "Too many requests to Azure service. Please try again later.",
                500 => "Azure service encountered an internal error. Please try again later.",
                _ => "An error occurred while accessing Azure services. Please try again or contact support."
            };

            await HandleExceptionAsync(context, messageForUser, ex.Status);

        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception information: {ex}");

            var messageForUser = "Internal Server Error. Please try again later or contact support.";

            await HandleExceptionAsync(context, messageForUser, StatusCodes.Status500InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, string messageForUser, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            Message = messageForUser,
        }));
    }
}
