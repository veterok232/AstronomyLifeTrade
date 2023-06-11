using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Common;

namespace ApplicationCore.Interfaces.Catalog.Management;

public interface ICreateProductService
{
    Task<Result<Guid>> Create(CreateProductModel model);
    
    Task<Result<Guid>> Edit(EditProductModel model);

    Task CreateCharacteristics(ProductCharacteristics model);
    
    Task EditCharacteristics(ProductCharacteristics model);
    
    Task<Result> Delete(Guid productId);
    
    Task<ProductForEditModel> GetProductForEditModel(Guid productId);
}