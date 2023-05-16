using ApplicationCore.Models.Identity;
using Infrastructure.Services.Identity.SecurityCheck;

namespace Infrastructure.Interfaces;

internal interface ISessionSecurityService
{
    Task<SessionValidationResult> ValidateSession(Guid refreshToken, ClientIdentificationData identificationData);
}