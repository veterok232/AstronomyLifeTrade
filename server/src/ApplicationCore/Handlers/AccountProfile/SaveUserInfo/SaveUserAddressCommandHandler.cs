using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.SaveUserInfo;

internal class SaveUserAddressCommandHandler : AsyncRequestHandler<SaveUserAddressCommand>
{
    private readonly IAccountProfileService _accountProfileService;
    private readonly IUnitOfWork _unitOfWork;

    public SaveUserAddressCommandHandler(
        IUnitOfWork unitOfWork,
        IAccountProfileService accountProfileService)
    {
        _unitOfWork = unitOfWork;
        _accountProfileService = accountProfileService;
    }

    protected override async Task Handle(SaveUserAddressCommand command, CancellationToken cancellationToken)
    {
        await _accountProfileService.SaveUserAddress(command.Model);
        await _unitOfWork.Commit();
    }
}