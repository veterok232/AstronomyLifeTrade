using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class ObjectForObservation : Entity
{
    public string Name { get; set; }
    
    public ObservationLevel Level { get; set; }
}