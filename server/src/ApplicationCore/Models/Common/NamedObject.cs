namespace ApplicationCore.Models.Common;

public class NamedObject : EntityModel
{
    public NamedObject()
    {
    }

    public NamedObject(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}