using System.Reflection;

namespace ApplicationCore.Models.ModelProtection;

public record PropertyAuthorizationInfo(PropertyInfo PropertyInfo)
{
    public string Permission { get; set; }

    public IEnumerable<PropertyAuthorizationInfo> Children { get; init; }
}