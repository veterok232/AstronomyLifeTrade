using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using AutoMapper;

namespace ApplicationCore;

public class ApplicationCoreMappingProfile : Profile
{
    public ApplicationCoreMappingProfile()
    {
        CreateMap<Product, ProductListItem>();
        CreateMap<Brand, BrandModel>();
        CreateMap<Category, CategoryModel>();
    }
}