using Api.Constants;
using Api.Controllers.Attributes;
using Api.Interfaces;
using ApplicationCore.Handlers.Catalog.GetPopularProducts;
using ApplicationCore.Handlers.Catalog.ProductDetails;
using ApplicationCore.Handlers.Catalog.SearchTelescopes;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
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
    
    [HttpGet(Routes.Catalog.ProductDetails)]
    [AllowAnonymous]
    public Task<ProductDetails> GetProductDetails(Guid productId)
    {
        return _mediator.Send(new GetProductDetailsQuery(productId));
    }
    
    /*[HttpGet(Routes.Catalog.GetProductRating)]
    [AllowAnonymous]
    public Task<ProductRatingModel> GetProductRating()
    {
        return _mediator.Send(new GetProductRatingRequest());
    }
    
    [HttpPost(Routes.Catalog.AddProductToCart)]
    [Authorization(Roles.Consumer)]
    public Task<SearchResult<ProductListItem>> AddProductToCart()
    {
        return _mediator.Send(new AddProductToCartCommand());
    }
    
    [HttpPost(Routes.Catalog.AddProductToFavorites)]
    [Authorization(Roles.Consumer)]
    public Task<SearchResult<ProductListItem>> AddProductToFavorites()
    {
        return _mediator.Send(new AddProductToFavoritesCommand());
    }*/
}