using CreateProjectOlive.Helper;
namespace CreateProjectOlive.Middleware
{

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
            // Add Your Method Here.
            SeedToDBHelper.SeedToDataToDb();
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("asy");
        }

    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
