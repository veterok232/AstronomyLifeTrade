using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.SaveUserInfo;

public record SaveUserInfoCommand(SaveUserInfoModel Model) : IRequest;