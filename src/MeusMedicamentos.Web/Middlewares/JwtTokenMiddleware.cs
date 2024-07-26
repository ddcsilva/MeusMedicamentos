public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Session.GetString("JWToken");

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", $"Bearer {token}");
        }

        await _next(context);
    }
}

public static class JwtTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtTokenMiddleware>();
    }
}
