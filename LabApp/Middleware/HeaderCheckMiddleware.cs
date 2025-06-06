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
            var path = context.Request.Path.ToString();
            var isGrpc = context.Request.ContentType?.StartsWith("application/grpc") == true;
            if (isGrpc || path == "/" || path.StartsWith("/swagger") || path.StartsWith("/api/Auth"))
            {
                await _next(context);
                return;
            }



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

