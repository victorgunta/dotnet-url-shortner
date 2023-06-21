using System.Globalization;
using dotnet_url_shortner.Services;

namespace dotnet_url_shortner.Middleware;

public class UserMetadataMiddleware
{
    private readonly RequestDelegate _next;    

    public UserMetadataMiddleware(RequestDelegate next)
    {
        _next = next;        
    }

    public async Task InvokeAsync(HttpContext context, StatService stat)
    {

        context.Request.EnableBuffering();

        var bodyAsText = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class UserMetadataMiddlewareExtensions
{
    public static IApplicationBuilder SaveUserMetadata(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserMetadataMiddleware>();
    }
}