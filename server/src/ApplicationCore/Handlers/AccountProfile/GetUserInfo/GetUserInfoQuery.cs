using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.GetUserInfo;

public record GetUserInfoQuery : IRequest<UserInfoModel>;