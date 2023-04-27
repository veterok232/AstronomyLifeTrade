using Api.Constants;
using ApplicationCore.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (PotentiallyConcurrentModificationsException)
        {
            HandleConcurrentModificationsException(context);
        }
        catch (DbUpdateConcurrencyException)
        {
            HandleConcurrentModificationsException(context);
        }
        catch (Exception ex)
        {
            string traceId = Guid.NewGuid().ToString();

            _logger.Log(LogLevel.Error, ex, $"TraceId: {traceId}");

            SetTraceableUnhandeledErrorResponse(context, traceId);
        }
    }

    private static void SetTraceableUnhandeledErrorResponse(HttpContext context, string traceId)
    {
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.Headers.Add(CustomHeaders.ExceptionTraceId, traceId);
    }

    private static void HandleConcurrentModificationsException(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status409Conflict;
    }
}