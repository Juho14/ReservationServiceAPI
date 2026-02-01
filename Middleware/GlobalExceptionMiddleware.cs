using System.Net;

namespace ConferenceRoom.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;


    public GlobalExceptionMiddleware(RequestDelegate next)
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
            LogException(context, ex);
            await HandleExceptionAsync(context);
        }
    }


    private static void LogException(HttpContext context, Exception ex)
    {
        Console.WriteLine("===== UNHANDLED EXCEPTION =====");
        Console.WriteLine($"Endpoint: {context.Request.Method} {context.Request.Path}");
        Console.WriteLine($"Exception: {ex.Message}");
        Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
        Console.WriteLine("Stack Trace:");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine("================================");
    }


    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";


        var response = new
        {
            message = "An unexpected error occurred. Please contact support if the problem persists."
        };


        return context.Response.WriteAsJsonAsync(response);
    }
}