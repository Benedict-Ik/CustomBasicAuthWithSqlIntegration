using CustomBasicAuth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace CustomBasicAuth.Helpers
{
    public class BasicAuthAttribute : ActionFilterAttribute
    {
        private const string AUTH_HEADER = "Authorization";
        private const string SCHEME = "Basic";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<BasicAuthAttribute>)) as ILogger;
            var db = context.HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var controller = actionDescriptor?.ControllerName;
            var action = actionDescriptor?.ActionName;

            // Validate header
            if (!context.HttpContext.Request.Headers.TryGetValue(AUTH_HEADER, out var authHeader))
            {
                SetUnauthorizedResult(context, logger, controller, action, "Missing Authorization header");
                return;
            }

            if (!authHeader.ToString().StartsWith($"{SCHEME} ", StringComparison.OrdinalIgnoreCase))
            {
                SetUnauthorizedResult(context, logger, controller, action, "Invalid authentication scheme");
                return;
            }

            // Decode credentials
            if (!TryDecodeCredentials(authHeader.ToString(), out var username, out var password))
            {
                SetUnauthorizedResult(context, logger, controller, action, "Invalid or malformed credentials");
                return;
            }

            // Check against database
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                SetUnauthorizedResult(context, logger, controller, action, "User not found");
                return;
            }

            var helper = new PasswordHashHelper();
            if (!helper.VerifyPassword(user, password))
            {
                SetUnauthorizedResult(context, logger, controller, action, "Invalid password");
                return;
            }

            logger?.LogInformation("User {Username} authenticated for {Controller}/{Action}.", username, controller, action);
        }

        private void SetUnauthorizedResult(ActionExecutingContext context, ILogger logger, string controller, string action, string reason)
        {
            logger?.LogWarning("{Reason} in {Controller}/{Action}.", reason, controller, action);
            context.Result = new ContentResult
            {
                StatusCode = 401,
                Content = reason
            };
        }

        private bool TryDecodeCredentials(string authHeader, out string username, out string password)
        {
            username = null;
            password = null;

            var encoded = authHeader.Substring(SCHEME.Length).Trim();

            try
            {
                var bytes = Convert.FromBase64String(encoded);
                var decoded = Encoding.UTF8.GetString(bytes);
                var parts = decoded.Split(':', 2);

                if (parts.Length != 2)
                {
                    return false;
                }

                username = parts[0];
                password = parts[1];
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}