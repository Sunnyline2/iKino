using Microsoft.AspNetCore.Authorization;

namespace eKino.Infrastructure
{
    public class RequireRole : AuthorizeAttribute
    {
        public RequireRole(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}