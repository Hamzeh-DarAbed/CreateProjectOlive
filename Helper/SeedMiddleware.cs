using CreateProjectOlive.Helper;
namespace CreateProjectOlive.SeedMiddleware
{

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedMiddleware(this IApplicationBuilder builder)
        {
            SeedToDBHelper.SeedToDataToDb();
            return builder;
        }
    }
}




// using CreateProjectOlive.Helper;
// namespace CreateProjectOlive.SeedMiddleware
// {

//     public class SeedMiddleware
//     {
//         private readonly RequestDelegate _next;

//         public SeedMiddleware(RequestDelegate next)
//         {
//             _next = next;
//             // Add Your Method Here.
//             SeedToDBHelper.SeedToDataToDb();
//         }

//         public async Task InvokeAsync(HttpContext context)
//         {
//             await _next(context);
//         }

//     }
//     public static class MiddlewareExtensions
//     {
//         public static IApplicationBuilder UseSeedMiddleware(this IApplicationBuilder builder)
//         {
//             return builder.UseMiddleware<SeedMiddleware>();
//         }
//     }
// }
