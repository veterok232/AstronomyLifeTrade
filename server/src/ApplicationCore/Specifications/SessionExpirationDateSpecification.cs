using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications;

internal class SessionExpirationDateSpecification : DataTransformSpecification<Session, DateTime>
{
    public SessionExpirationDateSpecification(Guid sessionId)
        : base(s => s.ExpiryDate, s => s.Id == sessionId)
    {
    }
}