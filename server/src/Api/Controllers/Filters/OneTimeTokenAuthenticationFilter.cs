using System.Net.Http.Headers;
using ApplicationCore.Enums;
using ApplicationCore.Handlers.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Api.Controllers.Filters;

internal class OneTimeTokenAuthenticationFilter : IAsyncAuthorizationFilter
{
    private const string AuthHeaderScheme = "OneTimeToken";
    private const string AuthUrlParameterName = "auth-token";

    private readonly OneTimeTokenTermType _tokenTermType;
    private readonly IMediator _mediator;

    public OneTimeTokenAuthenticationFilter(
        OneTimeTokenTermType tokenTermType,
        IMediator mediator)
    {
        _tokenTermType = tokenTermType;
        _mediator = mediator;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (TryGetToken(context.HttpContext, out var token))
        {
            var result = await _mediator.Send(
                new OneTimeTokenAuthenticationCommand
                {
                    Token = token,
                    TokenTermType = _tokenTermType,
                });

            if (result.IsSucceeded)
            {
                context.HttpContext.User = result.Data;
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new ForbidResult();
        }
    }

    private static bool TryGetToken(HttpContext context, out Guid token)
    {
        if (TryGetHeaderToken(context, out token) || TryGetUrlToken(context, out token))
        {
            return true;
        }

        token = default;
        return false;
    }

    private static bool TryGetHeaderToken(HttpContext context, out Guid token)
    {
        if (context.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeaders))
        {
            var header = authHeaders
                .Select(h => new
                {
                    Sucess = AuthenticationHeaderValue.TryParse(h, out var value),
                    Header = value,
                })
                .Where(h => h.Sucess && h.Header.Scheme == AuthHeaderScheme)
                .Select(h => h.Header.Parameter)
                .SingleOrDefault();

            if (header != null && Guid.TryParse(header, out token))
            {
                return true;
            }
        }

        token = default;
        return false;
    }

    private static bool TryGetUrlToken(HttpContext context, out Guid token)
    {
        if (context.Request.Query.TryGetValue(AuthUrlParameterName, out var tokenString) &&
            Guid.TryParse(tokenString, out token))
        {
            return true;
        }

        token = default;
        return false;
    }
}