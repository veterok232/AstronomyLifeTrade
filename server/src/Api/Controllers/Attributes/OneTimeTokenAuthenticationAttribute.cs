using Api.Controllers.Filters;
using ApplicationCore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Attributes;

internal sealed class OneTimeTokenAuthenticationAttribute : TypeFilterAttribute, IAllowAnonymous
{
    public OneTimeTokenAuthenticationAttribute(OneTimeTokenTermType tokenTermType)
        : base(typeof(OneTimeTokenAuthenticationFilter))
    {
        Arguments = new object[] { tokenTermType };
    }
}