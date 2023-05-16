using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Constants;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.Identity.SecurityCheck;

[ScopedDependency]
internal class ExpirationDateSessionValidator : ISessionSecurityValidator
{
    public ValidationResult Validate(Session session, ClientIdentificationData identificationData)
    {
        return Validate(session);
    }

    public ValidationResult Validate(Session session)
    {
        if (session.ExpiryDate.Add(IdentityConstants.ClockSkew) > DateTime.UtcNow)
        {
            return ValidationResult.CreateValid();
        }

        return ValidationResult.CreateInvalid("Session has expired");
    }
}