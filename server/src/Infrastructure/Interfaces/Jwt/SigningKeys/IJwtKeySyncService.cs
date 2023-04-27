namespace Infrastructure.Interfaces.Jwt.SigningKeys;

public interface IJwtKeySyncService
{
    Task Sync();
}