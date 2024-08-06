namespace LibraryApiService.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (userClaim != null)
                {
                    context.Response.Headers.Append("X-user", userClaim);
                }
            }

            await _next(context);
        }
    }
}
