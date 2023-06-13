using Api.Constants;
using Api.Controllers.Attributes;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.AccountProfile.GetUserInfo;
using ApplicationCore.Handlers.AccountProfile.SaveUserInfo;
using ApplicationCore.Handlers.AccountProfile.Statistics;
using ApplicationCore.Handlers.AccountProfile.UserManagement;
using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Common;
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
    
    [HttpGet(Routes.AccountProfile.GetOrdersStatistics)]
    [Authorization(Roles.Staff, Roles.Manager)]
    public Task<List<OrdersAggregatedDataItem>> GetOrdersStatistics([FromQuery] StatisticsQuery model)
    {
        return _mediator.Send(new GetOrdersStatisticsQuery(model));
    }
    
    [HttpGet(Routes.AccountProfile.GetManagersLookupItems)]
    [Authorization(Roles.Staff)]
    public Task<IReadOnlyList<NamedObject>> GetManagersLookupItems([FromQuery] ManagersLookupItemsQueryModel model)
    {
        return _mediator.Send(new ManagersLookupItemsQuery(model));
    }
    
    [HttpGet(Routes.AccountProfile.GetUsersLookupItems)]
    [Authorization(Roles.Staff)]
    public Task<IReadOnlyList<NamedObject>> GetUsersLookupItems([FromQuery] UsersLookupItemsQueryModel model)
    {
        return _mediator.Send(new UsersLookupItemsQuery(model));
    }
    
    [HttpPost(Routes.AccountProfile.AssignAsManager)]
    [Authorization(Roles.Staff)]
    public Task<Result> AssignAsManager(Guid userAssignmentId)
    {
        return _mediator.Send(new AssignAsManagerCommand(userAssignmentId));
    }
    
    [HttpPost(Routes.AccountProfile.AssignAsAdministrator)]
    [Authorization(Roles.Staff)]
    public Task<Result> AssignAsAdministrator(Guid userAssignmentId)
    {
        return _mediator.Send(new AssignAsAdministratorCommand(userAssignmentId));
    }
    
    [HttpPost(Routes.AccountProfile.BlockUser)]
    [Authorization(Roles.Staff)]
    public Task<Result> BlockUser(Guid userAssignmentId)
    {
        return _mediator.Send(new BlockUserCommand(userAssignmentId));
    }
    
    [HttpPost(Routes.AccountProfile.UnblockUser)]
    [Authorization(Roles.Staff)]
    public Task<Result> UnblockUser(Guid userAssignmentId)
    {
        return _mediator.Send(new UnblockUserCommand(userAssignmentId));
    }
}