using ApplicationCore.Entities;

namespace Infrastructure.Interfaces;

internal interface IAccessTokenService
{
    Task<string> CreateToken(Session session);
}