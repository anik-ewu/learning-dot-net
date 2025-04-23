using System.Reflection;
using UserManager.Application.Common.Attributes;

namespace UserManager.API.Middleware
{
    public class AdminAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var requiresAdmin = endpoint?.Metadata.GetMetadata<RequiresAdminAttribute>() != null;

            if (requiresAdmin)
            {
                // Simulating user role (in real projects, you'd get it from claims)
                var userIsAdmin = context.Request.Headers["X-User-Role"] == "Admin";

                if (!userIsAdmin)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied: Admins only");
                    return;
                }
            }

            await _next(context);
        }
    }
}
