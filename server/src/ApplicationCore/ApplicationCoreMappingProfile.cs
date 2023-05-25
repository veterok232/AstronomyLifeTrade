using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Cart;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Models.Identity;
using AutoMapper;

namespace ApplicationCore;

public class ApplicationCoreMappingProfile : Profile
{
    public ApplicationCoreMappingProfile()
    {
        CreateMap<Product, ProductListItem>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Id));
        CreateMap<Brand, BrandModel>();
        CreateMap<Category, CategoryModel>();
        CreateMap<TelescopeSearchModel, TelescopesSearchData>();
        CreateMap(typeof(SearchResult<>), typeof(SearchResult<>));
        CreateMap<UserRegistrationModel, User>();
        
        CreateMap<Cart, CartModel>()
            .ForMember(d => d.CartItems, o => o.MapFrom(s => s.CartItems));

        CreateMap<CartItem, CartItemModel>();
    }
}