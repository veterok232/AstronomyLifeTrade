namespace ApplicationCore.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class RequirePermissionAttribute : Attribute
{
    public RequirePermissionAttribute(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}