using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models;
using ApplicationCore.Models.Cart;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Models.Identity;
using ApplicationCore.Models.Orders;
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
        CreateMap<Address, AddressModel>();
        
        CreateMap<User, OrderCustomerInfo>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Assignment.PersonalData.Email))
            .ForMember(d => d.Phone, o => o.MapFrom(s => s.Assignment.PersonalData.Phone))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Assignment.PersonalData.Address));
        
        CreateMap<CartItem, OrderItem>()
            .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.Product.Price));
    }
}