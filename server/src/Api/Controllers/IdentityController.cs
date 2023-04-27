using Api.Constants;
using Api.Controllers.Attributes;
using Api.Interfaces;
using ApplicationCore.Handlers.Identity;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Models.Identity.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;

    public IdentityController(IMediator mediator, IRefreshTokenCookieService refreshTokenCookieService)
    {
        _mediator = mediator;
        _refreshTokenCookieService = refreshTokenCookieService;
    }

    [HttpPost(Routes.Identity.Login)]
    [AllowAnonymous]
    public async Task<Result> Login([FromBody] LoginData loginData)
    {
        var result = await _mediator.Send(new LoginCommand(loginData));
        if (result.IsSucceeded && result is Result<IdentityData> identityData)
        {
            _refreshTokenCookieService.SetCookie(
                HttpContext, identityData.Data.RefreshToken, identityData.Data.SessionExpirationDate);
        }

        return result;
    }

    [HttpPost(Routes.Identity.ChooseAssignment)]
    public async Task<IActionResult> ChooseAssignment([FromBody] ChangeAssignmentData data)
    {
        var refreshToken = _refreshTokenCookieService.GetCookie(HttpContext);
        if (refreshToken == null)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(
            new ChooseAssignmentCommand {RefreshToken = refreshToken.Value, Data = data});
        if (result.IsSucceeded && result is Result<IdentityData> identityData)
        {
            _refreshTokenCookieService.SetCookie(
                HttpContext, identityData.Data.RefreshToken, identityData.Data.SessionExpirationDate);
        }
        else
        {
            _refreshTokenCookieService.DeleteCookie(HttpContext);
        }

        return Ok(result);
    }

    [HttpPost(Routes.Identity.Logout)]
    public async Task<Unit> Logout()
    {
        _refreshTokenCookieService.DeleteCookie(HttpContext);

        return await _mediator.Send(new LogoutCommand());
    }

    [HttpPost(Routes.Identity.RefreshAccessToken)]
    [AllowAnonymous]
    public async Task<Result> RefreshToken([FromBody] ClientIdentificationData data)
    {
        var refreshToken = _refreshTokenCookieService.GetCookie(HttpContext);

        var result = await _mediator.Send(
            new RefreshTokenCommand {RefreshToken = refreshToken, ClientIdentificationData = data});

        if (result.IsSucceeded)
        {
            _refreshTokenCookieService.SetCookie(
                HttpContext, result.Data.RefreshToken, result.Data.SessionExpirationDate);
        }
        else
        {
            _refreshTokenCookieService.DeleteCookie(HttpContext);
        }

        return result;
    }

    [HttpPost(Routes.Identity.ExtendSession)]
    public async Task<IActionResult> ExtendSession([FromBody] ClientIdentificationData data)
    {
        var refreshToken = _refreshTokenCookieService.GetCookie(HttpContext);
        if (refreshToken == null)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(
            new ExtendSessionCommand {RefreshToken = refreshToken.Value, ClientIdentificationData = data});
        if (result.IsSucceeded)
        {
            _refreshTokenCookieService.SetCookie(HttpContext, result.Data.RefreshToken, result.Data.ExpiryDateTime);
        }
        else
        {
            _refreshTokenCookieService.DeleteCookie(HttpContext);
        }

        return Ok(result);
    }

    [HttpPost(Routes.Identity.CreateOneTimeToken)]
    public Task<Guid> CreateOneTimeToken(CreateOneTimeTokenCommand command)
    {
        return _mediator.Send(command);
    }
}