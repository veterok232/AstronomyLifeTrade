using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class ProductForEditModelByProductIdSpecification : DataTransformSpecification<Product, ProductForEditModel>
{
    public ProductForEditModelByProductIdSpecification(Guid productId)
        : base(
            p => new ProductForEditModel
            {
                Name = p.Name,
                Code = p.Code,
                Description = p.Description,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Manufacturer = p.Manufacturer,
                Quantity = p.Quantity,
                Equipment = p.Equipment,
                ShortDescription = p.ShortDescription,
                SpecialNote = p.SpecialNote,
                Characteristics = new ProductCharacteristics(),
            },
            p => p.Id == productId)
    {
    }
}