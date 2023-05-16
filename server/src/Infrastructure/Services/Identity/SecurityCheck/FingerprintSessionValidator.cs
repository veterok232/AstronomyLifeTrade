using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.Identity.SecurityCheck;

[ScopedDependency]
internal class FingerprintSessionValidator : ISessionSecurityValidator
{
    public ValidationResult Validate(Session session, ClientIdentificationData identificationData)
    {
        if (string.Equals(identificationData.Fingerprint, session.Fingerprint, StringComparison.Ordinal))
        {
            return ValidationResult.CreateValid();
        }

        return ValidationResult.CreateInvalid(
            $"Fingerprint '{identificationData.Fingerprint}' does not match session fingerprint '{session.Fingerprint}'");
    }
}