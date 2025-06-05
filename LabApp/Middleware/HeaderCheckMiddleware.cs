namespace LabApp.Middleware
{
    public class HeaderCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            if (!context.Request.Headers.ContainsKey("X-App-Client"))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Brakuje nagłówka X-App-Client");
                return;
            }

            await _next(context);
        }
    }

    public static class HeaderCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderCheckMiddleware>();
        }
    }
}

