using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.AstronomicalCalculator;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.AstronomicalCalculator;

public class TelescopesForCalculatorSpecification : DataTransformSpecification<Telescope, ProductListItem>
{
    public TelescopesForCalculatorSpecification(AstronomicalCalculatorMostMatchingModel model)
        : base(t => new ProductListItem
            {
                ProductId = t.ProductId,
                Brand = new BrandModel
                {
                    Id = t.Product.BrandId,
                    Name = t.Product.Brand.Name,
                    CategoryType = t.Product.Brand.CategoryType,
                },
                Category = new CategoryModel
                {
                    Id = t.Product.CategoryId,
                    Name = t.Product.Name,
                    Code = t.Product.Category.Code,
                    Description = t.Product.Category.Description,
                    ProductsCount = t.Product.Category.ProductsCount,
                },
                Price = t.Product.Price,
                Quantity = t.Product.Quantity,
                Name = t.Product.Name,
                ShortDescription = t.Product.ShortDescription,
                SpecialNote = t.Product.SpecialNote,
                Code = t.Product.Code,
                CharacteristicsModels = new List<CharacteristicModel>
                {
                    new CharacteristicModel
                    {
                        Name = "Апертура",
                        Value = t.Aperture.ToString(),
                    },
                    new CharacteristicModel
                    {
                        Name = "Фокусное расстояние",
                        Value = t.FocusDistance.ToString(),
                    },
                    new CharacteristicModel
                    {
                        Name = "Полезное увеличение",
                        Value = t.MaxUsefulScale.ToString(),
                    }
                },
                ProductRating = new ProductRatingModel
                {
                    CommentsCount = t.Product.Comments != null
                        ? t.Product.Comments.Count
                        : 0,
                    Rating = t.Product.Comments != null
                        ? t.Product.Comments.Select(c => c.Rating).Any()
                            ? t.Product.Comments.Select(c => c.Rating).Average()
                            : 0
                        : 0,
                },
                ImageFilesIds = t.Product.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .OrderBy(pf => pf.File.FileName)
                    .Select(f => f.FileId).ToList(),
            },
            t => (t.Aperture == model.Aperture &&
                 t.FocusDistance == model.FocusDistance) ||
                 Math.Abs(t.MaxUsefulScale.Value - model.MaxScale) <= 40 ||
                 Math.Abs(t.ScaleMax.Value - model.MaxScale) <= 40)
    {
        AddInclude(sp => sp.Product);
        AddInclude(sp => sp.Product.Brand);
        AddInclude(sp => sp.Product.Category);
        AddInclude(sp => sp.Product.Comments);
        AddInclude(sp => sp.Product.Files);
        
        ApplyPaging(1, 5);
    }
}