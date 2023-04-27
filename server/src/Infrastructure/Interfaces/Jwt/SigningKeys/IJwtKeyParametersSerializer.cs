using System.Security.Cryptography;

namespace Infrastructure.Interfaces.Jwt.SigningKeys;

internal interface IJwtKeyParametersSerializer
{
    string Serialize(RSAParameters parameters);

    RSAParameters Deserialize(string serializedParameters);
}