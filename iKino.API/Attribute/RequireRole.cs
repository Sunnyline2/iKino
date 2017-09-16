﻿using Microsoft.AspNetCore.Authorization;

namespace iKino.API.Attribute
{
    public class RequireRole : AuthorizeAttribute
    {
        public RequireRole(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
