using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.Identity.SecurityCheck;

[ScopedDependency]
internal class InvalidationSessionValidator : ISessionSecurityValidator
{
    public ValidationResult Validate(Session session, ClientIdentificationData identificationData)
    {
        return Validate(session);
    }

    public ValidationResult Validate(Session session)
    {
        if (!session.Invalidated)
        {
            return ValidationResult.CreateValid();
        }

        return ValidationResult.CreateInvalid("The session was invalidated");
    }
}