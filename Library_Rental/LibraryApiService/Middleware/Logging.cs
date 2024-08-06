namespace LibraryApiService.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Linq;
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
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "user_id");
                if (userIdClaim != null)
                {
                    context.Response.Headers.Add("X-User", userIdClaim.Value);
                }
            }

            await _next(context);
        }
    }
}
