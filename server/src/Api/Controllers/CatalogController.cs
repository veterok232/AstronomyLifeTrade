using Api.Constants;
using Api.Controllers.Attributes;
using Api.Interfaces;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.Catalog.CreateProduct;
using ApplicationCore.Handlers.Catalog.DeleteProduct;
using ApplicationCore.Handlers.Catalog.EditProduct;
using ApplicationCore.Handlers.Catalog.Get;
using ApplicationCore.Handlers.Catalog.GetPopularProducts;
using ApplicationCore.Handlers.Catalog.GetProductForEdit;
using ApplicationCore.Handlers.Catalog.ProductDetails;
using ApplicationCore.Handlers.Catalog.SearchAccessories;
using ApplicationCore.Handlers.Catalog.SearchBinoculars;
using ApplicationCore.Handlers.Catalog.SearchProducts;
using ApplicationCore.Handlers.Catalog.SearchTelescopes;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator, IRefreshTokenCookieService refreshTokenCookieService)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Routes.Catalog.GetPopularProducts)]
    [AllowAnonymous]
    public Task<ICollection<ProductListItem>> GetPopularProducts()
    {
        return _mediator.Send(new GetPopularProductsRequest());
    }

    [HttpGet(Routes.Catalog.SearchTelescopes)]
    [AllowAnonymous]
    public Task<SearchResult<ProductListItem>> SearchTelescopes([FromQuery] TelescopeSearchModel model)
    {
        return _mediator.Send(new SearchTelescopesRequest(model));
    }
    
    [HttpGet(Routes.Catalog.SearchBinoculars)]
    [AllowAnonymous]
    public Task<SearchResult<ProductListItem>> SearchBinoculars([FromQuery] BinocularSearchModel model)
    {
        return _mediator.Send(new SearchBinocularsRequest(model));
    }
    
    [HttpGet(Routes.Catalog.SearchAccessories)]
    [AllowAnonymous]
    public Task<SearchResult<ProductListItem>> SearchAccessories([FromQuery] AccessoriesSearchModel model)
    {
        return _mediator.Send(new SearchAccessoriesRequest(model));
    }
    
    [HttpGet(Routes.Catalog.SearchProducts)]
    [AllowAnonymous]
    public Task<SearchResult<ProductListItem>> SearchProducts([FromQuery] ProductsSearchModel model)
    {
        return _mediator.Send(new SearchProductsRequest(model));
    }
    
    [HttpGet(Routes.Catalog.ProductDetails)]
    [AllowAnonymous]
    public Task<ProductDetails> GetProductDetails(Guid productId)
    {
        return _mediator.Send(new GetProductDetailsQuery(productId));
    }

    [HttpGet(Routes.Catalog.GetBrands)]
    [AllowAnonymous]
    public Task<ICollection<BrandModel>> GetBrands()
    {
        return _mediator.Send(new GetBrandsQuery());
    }
    
    [HttpGet(Routes.Catalog.GetCategories)]
    [AllowAnonymous]
    public Task<ICollection<CategoryModel>> GetCategories()
    {
        return _mediator.Send(new GetCategoriesQuery());
    }
    
    [HttpPost(Routes.Catalog.CreateProduct)]
    [Authorization(Roles.Staff)]
    public Task<Result<Guid>> CreateProduct([FromForm] CreateProductModel model)
    {
        return _mediator.Send(new CreateProductCommand(model));
    }
    
    [HttpPost(Routes.Catalog.EditProduct)]
    [Authorization(Roles.Staff)]
    public Task<Result<Guid>> EditProduct([FromForm] EditProductModel model)
    {
        return _mediator.Send(new EditProductCommand(model));
    }
    
    [HttpPost(Routes.Catalog.DeleteProduct)]
    [Authorization(Roles.Staff)]
    public Task<Result> DeleteProduct(Guid productId)
    {
        return _mediator.Send(new DeleteProductCommand(productId));
    }
    
    [HttpPost(Routes.Catalog.EditProductCharacteristics)]
    [Authorization(Roles.Staff)]
    public Task EditProductCharacteristics([FromBody] ProductCharacteristics model)
    {
        return _mediator.Send(new EditProductCharacteristicsCommand(model));
    }
    
    [HttpGet(Routes.Catalog.ProductForEdit)]
    [Authorization(Roles.Staff)]
    public Task<ProductForEditModel> ProductForEdit(Guid productId)
    {
        return _mediator.Send(new GetProductForEditQuery(productId));
    }
    
    [HttpPost(Routes.Catalog.CreateProductCharacteristics)]
    [Authorization(Roles.Staff)]
    public Task CreateProductCharacteristics([FromBody] ProductCharacteristics model)
    {
        return _mediator.Send(new CreateProductCharacteristicsCommand(model));
    }
}