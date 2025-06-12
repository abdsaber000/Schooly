using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace SchoolManagement.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var method = request.Method;
        var path = request.Path;
        var query = request.QueryString.ToString();

        var headers = request.Headers
            .Where(h => !h.Key.ToLower().Contains("authorization") && !h.Key.ToLower().Contains("cookie"))
            .ToDictionary(h => h.Key, h => h.Value.ToString());

        var user = context.User;
        var userName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "name")?.Value ?? "Anonymous";
        var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value ?? "No Email";

        _logger.LogInformation("User authenticated: {IsAuthenticated}", user?.Identity?.IsAuthenticated);
        _logger.LogInformation("User name: {UserName}", user?.Identity?.Name);
        _logger.LogInformation("User email: {Email}", email);

        string body = string.Empty;
        if (request.ContentLength > 0 && 
            request.ContentType != null && 
            !request.ContentType.Contains("multipart/form-data"))
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            const int maxBodyLength = 10240;
            if (body.Length > maxBodyLength)
            {
                body = body.Substring(0, maxBodyLength) + "...";
            }
        }

        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        _logger.LogInformation(
            "Request by {User} (Email: {Email}): {Method} {Path} {Query}\nHeaders: {@Headers}\nBody: {Body}\nDuration: {Duration} ms",
            userName, email, method, path, query, headers, body, stopwatch.ElapsedMilliseconds
        );
    }
}