using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public readonly IList<role> _roles;

        public AuthorizeAttribute(params role[] roles)
        {
            _roles = roles ?? new role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            try
            {
                var test = context.HttpContext.User.Claims.First(x => x.Type == "id").Value;
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(Result.Unauthorized("ამ სერვისზე წვდომა შეუძლებელია ავტორიზაციის გარეშე")) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var role = int.Parse(context.HttpContext.User.Claims.First(x => x.Type == "roleKay").Value);

            if (_roles != null && _roles.Any() && !_roles.Any(x => (int)x == role))
            {
                context.Result = new JsonResult(Result.AccessDenied("მოცემულ მომხმარებელს არ აქვს უფლება ამ სერვისის გამოყენების")) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
