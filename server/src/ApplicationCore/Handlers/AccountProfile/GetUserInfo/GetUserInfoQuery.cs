using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile;

public record GetUserInfoQuery : IRequest<UserInfoModel>;