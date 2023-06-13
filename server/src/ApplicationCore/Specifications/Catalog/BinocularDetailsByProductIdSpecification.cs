using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Files;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class BinocularDetailsByProductIdSpecification : DataTransformSpecification<Binocular, BinocularDetails>
{
    public BinocularDetailsByProductIdSpecification(Guid productId)
        : base(
            t => new BinocularDetails
            {
                Brand = new BrandModel
                {
                    Id = t.Product.Brand.Id,
                    Name = t.Product.Brand.Name,
                },
                Category = new CategoryModel
                {
                    Id = t.Product.Category.Id,
                    Name = t.Product.Category.Name,
                    Code = t.Product.Category.Code,
                    Description = t.Product.Category.Description,
                    ProductsCount = t.Product.Category.ProductsCount,
                },
                Characteristics = new List<CharacteristicModel>
                {
                    new CharacteristicModel
                    {
                        Name = "Увеличение",
                        Value = t.ScaleMax.ToString(),
                    },
                    new CharacteristicModel
                    {
                        Name = "Поле зрения",
                        Value = t.FovMax.ToString(),
                    },
                },
                Code = t.Product.Code,
                Price = t.Product.Price,
                Manufacturer = t.Product.Manufacturer,
                Quantity = t.Product.Quantity,
                Name = t.Product.Name,
                ShortDescription = t.Product.ShortDescription,
                Description = t.Product.Description,
                Equipment = t.Product.Equipment,
                Rating = new ProductRatingModel
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
                Aperture = t.Aperture,
                ExitPupilDiameterMax = t.ExitPupilDiameterMax,
                ExitPupilDiameterMin = t.ExitPupilDiameterMin,
                FocusingMethod = t.FocusingMethod,
                FovMin = t.FovMin,
                FovMax = t.FovMax,
                HasAdapter = t.HasAdapter,
                HasCase = t.HasCase,
                HasMoistureProtection = t.HasMoistureProtection,
                InterpupillaryDistanseMin = t.InterpupillaryDistanseMin,
                InterpupillaryDistanseMax = t.InterpupillaryDistanseMax,
                FocusDistanceMin = t.FocusDistanceMin,
                OpticsMaterial = t.OpticsMaterial,
                PrismType = t.PrismType,
                BinocularPurpose = t.Purpose,
                RelativeBrightnessMin = t.RelativeBrightnessMin,
                RelativeBrightnessMax = t.RelativeBrightnessMax,
                RemovalExitPupilMin = t.RemovalExitPupilMin,
                RemovalExitPupilMax = t.RemovalExitPupilMax,
                ScaleMin = t.ScaleMin,
                ScaleMax = t.ScaleMax,
                Weight = t.Weight,
                ProductImagesIds = t.Product.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .OrderBy(pf => pf.File.FileName)
                    .Select(f => f.FileId).ToList(),
                ProductFiles = t.Product.Files
                    .Where(f => f.ProductFileType == ProductFileType.File)
                    .OrderBy(pf => pf.File.FileName)
                    .Select(f => new FileModel
                    {
                        Id = f.FileId,
                        Name = f.File.FullFileName,
                    }).ToList(),
            },
            t => t.ProductId == productId)
    {
    }
}