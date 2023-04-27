using Api.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Attributes;

public sealed class AuthorizationAttribute : TypeFilterAttribute
{
    public AuthorizationAttribute(params string[] args)
        : base(typeof(AuthorizationFilter))
    {
        Arguments = new object[] { args };
    }
}