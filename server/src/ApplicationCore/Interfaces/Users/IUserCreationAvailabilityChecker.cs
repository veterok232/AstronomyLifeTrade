using ApplicationCore.Models.Common;

namespace ApplicationCore.Interfaces.Users;

internal interface IUserCreationAvailabilityChecker
{
    Task<Result> Check(string email);
}