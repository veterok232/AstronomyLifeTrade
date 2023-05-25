using System.Security.Cryptography;
using System.Text;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces.Jwt.SigningKeys;
using Newtonsoft.Json;

namespace Infrastructure.Services.Jwt.SigningKeys;

[ScopedDependency]
public class JwtKeyParametersSerializer : IJwtKeyParametersSerializer
{
    public string Serialize(RSAParameters parameters)
    {
        return Convert.ToBase64String(
            Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(parameters)));
    }

    public RSAParameters Deserialize(string serializedParameters)
    {
        return JsonConvert.DeserializeObject<RSAParameters>(
            Encoding.UTF8.GetString(
                Convert.FromBase64String(serializedParameters)));
    }
}