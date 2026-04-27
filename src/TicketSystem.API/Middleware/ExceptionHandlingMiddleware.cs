using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Domain.Exceptions;
namespace TicketSystem.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private static Task HandleAsync(HttpContext context, Exception ex)
    {
        var (statusCode, message) = ex switch
        {
            DomainException e => (400, e.Message),
            ValidationException e => (400, e.Message),
            NotFoundException e => (404, e.Message),
            UnauthorizedException e => (401, e.Message),
            ConflictException e => (409, e.Message),
            _ => (500, "An unexpected error occurred.")
        };
        
        Console.WriteLine(ex);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var body = System.Text.Json.JsonSerializer.Serialize(new { error = message });
        return context.Response.WriteAsync(body);
    }
}