using ApplicationCore.Models;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.SaveUserInfo;

public record SaveUserAddressCommand(AddressModel Model) : IRequest;