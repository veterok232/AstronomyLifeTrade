using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;

namespace Infrastructure.Services.Identity.SecurityCheck;

[ScopedDependency]
internal class SessionSecurityService : ISessionSecurityService
{
    private readonly IRepository<Session> _sessionRepository;
    private readonly ISessionManager _sessionManager;
    private readonly IEnumerable<ISessionSecurityValidator> _securityValidators;
    private readonly IAppLogger<SessionSecurityService> _logger;

    public SessionSecurityService(
        IRepository<Session> sessionRepository,
        ISessionManager sessionManager,
        IEnumerable<ISessionSecurityValidator> securityValidators,
        IAppLogger<SessionSecurityService> logger)
    {
        _sessionRepository = sessionRepository;
        _sessionManager = sessionManager;
        _securityValidators = securityValidators;
        _logger = logger;
    }

    public async Task<SessionValidationResult> ValidateSession(
        Guid refreshToken, ClientIdentificationData identificationData)
    {
        var session =
            await _sessionRepository.GetSingleOrDefault(new SessionForAuthorizationSpecification(refreshToken));
        var validationResult = ValidateSession(session, identificationData);
        if (validationResult.IsValid)
        {
            return SessionValidationResult.CreateValid(session);
        }

        await _sessionManager.InvalidateUserSessions(session?.UserId ?? identificationData.UserId);
        LogValidationFailure(validationResult, identificationData);

        return SessionValidationResult.CreateInvalid(session);
    }

    private ValidationResult ValidateSession(Session session, ClientIdentificationData identificationData)
    {
        if (session == null)
        {
            return ValidationResult.CreateInvalid("Unable to find session in the database");
        }

        return ValidationResult.CreateFromErrors(
            _securityValidators.Select(v => v.Validate(session, identificationData))
                .Where(r => !r.IsValid)
                .SelectMany(r => r.Errors)
                .ToList());
    }

    private void LogValidationFailure(ValidationResult validationResult, ClientIdentificationData identificationData)
    {
        _logger.Warn(
            $"Session validation failed for user '{identificationData.UserId}' with fingerprint '{identificationData.Fingerprint}'. " +
            $"User sessions were invalidated. Reasons: {string.Join("; ", validationResult.Errors)}");
    }
}