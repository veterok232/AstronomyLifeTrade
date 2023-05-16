using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.Identity.SecurityCheck;

[ScopedDependency]
internal class UserIdSessionValidator : ISessionSecurityValidator
{
    public ValidationResult Validate(Session session, ClientIdentificationData identificationData)
    {
        if (session.UserId == identificationData.UserId)
        {
            return ValidationResult.CreateValid();
        }

        return ValidationResult.CreateInvalid(
            $"User Id '{identificationData.UserId}' does not match with session User Id '{session.UserId}'");
    }
}