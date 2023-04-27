using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Promotion : Entity, IHasVersion
{
    public string Name { get; set; }

    public bool IsSpecial { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public Guid Version { get; set; }
}