namespace PerfumeShop.Web.Utilities;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EmailException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
        catch (NullReferenceException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
