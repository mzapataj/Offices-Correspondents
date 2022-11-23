using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Prueba.WebServices.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AntiforgeryMiddleware : IMiddleware
    {
        private readonly IAntiforgery _antiforgery;

        public AntiforgeryMiddleware(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var isGetRequest = string.Equals("GET", context.Request.Method, StringComparison.OrdinalIgnoreCase);
            if (!isGetRequest)
            {
                _antiforgery.ValidateRequestAsync(context).GetAwaiter().GetResult();
            }

            await next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AntiforgeryMiddlewareExtensions
    {
        public static IApplicationBuilder UseAntiforgeryMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiforgeryMiddleware>();
        }
    }
}
