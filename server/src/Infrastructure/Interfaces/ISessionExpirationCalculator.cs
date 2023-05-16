using ApplicationCore.Entities;

namespace Infrastructure.Interfaces;

internal interface ISessionExpirationCalculator
{
    DateTime GetExpirationTime(Assignment assignment);
}