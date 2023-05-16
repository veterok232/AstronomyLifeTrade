using ApplicationCore.Entities;

namespace Infrastructure.Services.Identity.SecurityCheck;

public class SessionValidationResult
{
    private SessionValidationResult()
    {
    }

    public bool IsValid { get; set; }

    public Session Session { get; set; }

    public static SessionValidationResult CreateValid(Session session)
    {
        return new SessionValidationResult { IsValid = true, Session = session };
    }

    public static SessionValidationResult CreateInvalid(Session session)
    {
        return new SessionValidationResult { IsValid = false, Session = session };
    }
}