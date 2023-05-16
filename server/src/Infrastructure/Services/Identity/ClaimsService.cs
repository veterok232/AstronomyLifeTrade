using System.Security.Claims;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces;

namespace Infrastructure.Services.Identity;

[ScopedDependency]
internal class ClaimsService : IClaimsService
{
    public async Task<IEnumerable<Claim>> CreateClaims(Session session)
    {
        var builder = new ClaimsBuilder();
        builder.AddSessionClaims(session)
            .AddUserClaims(session.User)
            .AddAssignmentClaims(session.Assignment);

        return builder.Build();
    }
}