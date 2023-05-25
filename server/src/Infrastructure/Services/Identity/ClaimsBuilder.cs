using System.Security.Claims;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Services.Identity;

internal class ClaimsBuilder
{
    private readonly List<Claim> _claims;

    public ClaimsBuilder()
    {
        _claims = new List<Claim>();
    }

    public ClaimsBuilder AddSessionClaims(Session session)
    {
        _claims.Add(new Claim(JwtRegisteredClaimNames.Sid, session.Id.ToString()));

        return this;
    }

    public ClaimsBuilder AddUserClaims(User user)
    {
        _claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
        _claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        _claims.Add(new Claim(CustomClaimTypes.UserId, user.Id.ToString()));

        return this;
    }

    public ClaimsBuilder AddAssignmentClaims(Assignment assignment)
    {
        if (assignment == null)
        {
            return this;
        }

        _claims.Add(new Claim(CustomClaimTypes.AssignmentId, assignment.Id.ToString()));
        _claims.Add(new Claim(ClaimTypes.Role, assignment.Role.Name));
        _claims.Add(new Claim(CustomClaimTypes.Privilege, assignment.Role.Name));

        return this;
    }

    public IEnumerable<Claim> Build()
    {
        return _claims;
    }
}