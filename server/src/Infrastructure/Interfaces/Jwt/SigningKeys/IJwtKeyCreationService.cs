namespace Infrastructure.Interfaces.Jwt.SigningKeys;

public interface IJwtKeyCreationService
{
    Task Create();
}