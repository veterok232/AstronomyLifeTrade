namespace ApplicationCore.Models.Common;

public class VersionedEntityModel : EntityModel
{
    public Guid Version { get; set; }
}