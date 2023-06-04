using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile;

internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoModel>
{
    private readonly IAccountProfileService _accountProfileService;

    public GetUserInfoQueryHandler(IAccountProfileService accountProfileService)
    {
        _accountProfileService = accountProfileService;
    }

    public async Task<UserInfoModel> Handle(
        GetUserInfoQuery query,
        CancellationToken cancellationToken)
    {
        return await _accountProfileService.GetUserInfo();
    }
}