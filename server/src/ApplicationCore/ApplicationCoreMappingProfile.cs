using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Cart;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using ApplicationCore.Models.Comments;
using ApplicationCore.Models.Identity;
using ApplicationCore.Models.Orders;
using AutoMapper;

namespace ApplicationCore;

public class ApplicationCoreMappingProfile : Profile
{
    public ApplicationCoreMappingProfile()
    {
        CreateMap<Product, ProductListItem>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Id))
            .ForMember(
                d => d.ImageFilesIds,
                o => o.MapFrom(s => s.Files.Where(pf => pf.ProductFileType == ProductFileType.Image).Select(pf => pf.FileId).ToList()));
        CreateMap<Brand, BrandModel>();
        CreateMap<Category, CategoryModel>();
        CreateMap<TelescopeSearchModel, TelescopesSearchData>();
        CreateMap(typeof(SearchResult<>), typeof(SearchResult<>));
        CreateMap<UserRegistrationModel, User>();
        
        CreateMap<Cart, CartModel>()
            .ForMember(d => d.CartItems, o => o.MapFrom(s => s.CartItems));

        CreateMap<CartItem, CartItemModel>();
        CreateMap<Address, AddressModel>();
        CreateMap<AddressModel, Address>();
        
        CreateMap<User, OrderCustomerInfo>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Assignment.PersonalData.Email))
            .ForMember(d => d.Phone, o => o.MapFrom(s => s.Assignment.PersonalData.Phone))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Assignment.PersonalData.Address));
        
        CreateMap<CartItem, OrderItem>()
            .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.Product.Price));
        
        CreateMap<SaveUserInfoModel, PersonalData>()
            .ForMember(d => d.Birthday, o => o.MapFrom(s => s.Birthday.SetKindUtc()));
        
        CreateMap<OrdersSearchModel, OrdersSearchData>();
        
        CreateMap<Order, OrderDetailsModel>()
            .ForMember(d => d.Quantity, o => o.MapFrom(s => s.OrderItems.Count))
            .ForMember(d => d.CustomerFirstName, o => o.MapFrom(s => s.ConsumerAssignment.PersonalData.FirstName))
            .ForMember(d => d.CustomerLastName, o => o.MapFrom(s => s.ConsumerAssignment.PersonalData.LastName))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.ConsumerAssignment.PersonalData.Address));
        
        CreateMap<OrderItem, OrderItemModel>();
        
        CreateMap<Comment, CommentModel>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.Assignment.PersonalData.FirstName))
            .ForMember(d => d.UserLastName, o => o.MapFrom(s => s.Assignment.PersonalData.LastName));

        CreateMap<CommentModel, Comment>();
    }
}