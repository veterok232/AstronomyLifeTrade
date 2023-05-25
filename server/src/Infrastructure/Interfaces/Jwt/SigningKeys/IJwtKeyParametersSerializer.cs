using System.Security.Cryptography;

namespace Infrastructure.Interfaces.Jwt.SigningKeys;

public interface IJwtKeyParametersSerializer
{
    string Serialize(RSAParameters parameters);

    RSAParameters Deserialize(string serializedParameters);
}