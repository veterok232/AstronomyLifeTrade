using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Search;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;

namespace ApplicationCore.Services.Catalog.Search;

[ScopedDependency]
public class FillCharacteristicsService : IFillCharacteristicsService
{
    private readonly IRepository<Telescope> _telescopesRepository;
    private readonly IRepository<Binocular> _binocularsRepository;

    public FillCharacteristicsService(
        IRepository<Telescope> telescopesRepository,
        IRepository<Binocular> binocularsRepository)
    {
        _telescopesRepository = telescopesRepository;
        _binocularsRepository = binocularsRepository;
    }

    public async Task FillCharacteristics(ICollection<ProductListItem> products)
    {
        foreach (var product in products)
        {
            switch (product.Category.Name)
            {
                case "Telescopes":
                    var telescope = await _telescopesRepository.GetSingleOrDefault(
                        new TelescopeByProductIdSpecification(product.ProductId));

                    product.CharacteristicsModels = new List<CharacteristicModel>
                    {
                        new CharacteristicModel
                        {
                            Name = "Апертура",
                            Value = telescope.Aperture?.ToString(),
                        },
                        new CharacteristicModel
                        {
                            Name = "Фокусное расстояние",
                            Value = telescope.FocusDistance?.ToString(),
                        },
                        new CharacteristicModel
                        {
                            Name = "Полезное увеличение",
                            Value = telescope.MaxUsefulScale?.ToString(),
                        }
                    };
                    
                    break;
                case "Binoculars":
                    var binocular = await _binocularsRepository.GetSingleOrDefault(
                        new BinocularByProductIdSpecification(product.ProductId));

                    product.CharacteristicsModels = new List<CharacteristicModel>
                    {
                        new CharacteristicModel
                        {
                            Name = "Увеличение",
                            Value = binocular.ScaleMax.ToString(),
                        },
                        new CharacteristicModel
                        {
                            Name = "Поле зрения",
                            Value = binocular.FovMax.ToString(),
                        },
                    };
                    
                    break;
            }
        }
    }
}