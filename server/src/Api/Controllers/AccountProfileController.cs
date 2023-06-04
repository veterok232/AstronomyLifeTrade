using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.AccountProfile;
using ApplicationCore.Handlers.AccountProfile.SaveUserInfo;
using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class AccountProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.AccountProfile.GetUserInfo)]
    [Authorization(Roles.Consumer)]
    public Task<UserInfoModel> GetUserInfo()
    {
        return _mediator.Send(new GetUserInfoQuery());
    }
    
    [HttpPost(Routes.AccountProfile.SaveUserInfo)]
    [Authorization(Roles.Consumer)]
    public Task SaveUserInfo([FromBody] SaveUserInfoModel model)
    {
        return _mediator.Send(new SaveUserInfoCommand(model));
    }
    
    [HttpPost(Routes.AccountProfile.SaveUserAddress)]
    [Authorization(Roles.Consumer)]
    public Task SaveUserAddress([FromBody] AddressModel model)
    {
        return _mediator.Send(new SaveUserAddressCommand(model));
    }
}