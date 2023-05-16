using System.Security.Claims;
using ApplicationCore.Entities;

namespace Infrastructure.Interfaces;

internal interface IClaimsService
{
    Task<IEnumerable<Claim>> CreateClaims(Session session);
}