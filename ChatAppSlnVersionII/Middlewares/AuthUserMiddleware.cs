using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatAppSlnVersionII.Middlewares
{
    public class AuthUserMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (idClaim != null)
                {
                    httpContext.Items["UserId"] = idClaim.Value;
                }
            }
            await _next(httpContext);
        }
    }
}
