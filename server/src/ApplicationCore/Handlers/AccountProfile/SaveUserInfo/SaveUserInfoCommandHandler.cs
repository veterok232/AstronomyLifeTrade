using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.SaveUserInfo;

internal class SaveUserInfoCommandHandler : AsyncRequestHandler<SaveUserInfoCommand>
{
    private readonly IAccountProfileService _accountProfileService;
    private readonly IUnitOfWork _unitOfWork;

    public SaveUserInfoCommandHandler(
        IUnitOfWork unitOfWork,
        IAccountProfileService accountProfileService)
    {
        _unitOfWork = unitOfWork;
        _accountProfileService = accountProfileService;
    }

    protected override async Task Handle(SaveUserInfoCommand command, CancellationToken cancellationToken)
    {
        await _accountProfileService.SaveUserInfo(command.Model);
        await _unitOfWork.Commit();
    }
}