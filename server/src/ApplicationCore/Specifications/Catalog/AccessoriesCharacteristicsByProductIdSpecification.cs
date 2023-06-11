using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class AccessoriesCharacteristicsByProductIdSpecification : DataTransformSpecification<Accessory, AccessoryCharacteristics>
{
    public AccessoriesCharacteristicsByProductIdSpecification(Guid productId)
        : base(
            a => new AccessoryCharacteristics
            {
                AccessoryType = a.AccessoryType,
            },
            a => a.ProductId == productId)
    {
    }
}