using System;
using System.Text.Json;

namespace SchoolManagement.Api.Middlewares;
public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            
            var problemDetails = new 
            {
                message = "Unauthorized access"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

    }
}
