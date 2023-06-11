using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;
using AutoMapper;

namespace ApplicationCore.Services.Catalog;

[ScopedDependency]
public class CatalogService : ICatalogService
{
    private readonly IRepository<Brand> _brandsRepository;
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IRepository<Category> categoriesRepository,
        IRepository<Brand> brandsRepository,
        IMapper mapper)
    {
        _categoriesRepository = categoriesRepository;
        _brandsRepository = brandsRepository;
        _mapper = mapper;
    }


    public async Task<ICollection<BrandModel>> GetBrands()
    {
        return _mapper.Map<ICollection<BrandModel>>(await _brandsRepository.ListAll());
    }

    public async Task<ICollection<CategoryModel>> GetCategories()
    {
        return _mapper.Map<ICollection<CategoryModel>>(await _categoriesRepository.ListAll());
    }
}