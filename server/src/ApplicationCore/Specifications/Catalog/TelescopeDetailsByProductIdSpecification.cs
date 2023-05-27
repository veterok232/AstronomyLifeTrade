using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class TelescopeDetailsByProductIdSpecification : DataTransformSpecification<Telescope, TelescopeDetails>
{
    public TelescopeDetailsByProductIdSpecification(Guid productId)
        : base(
            t => new TelescopeDetails
            {
                Brand = new BrandModel
                {
                    BrandId = t.Product.Brand.Id,
                    BrandName = t.Product.Brand.Name,
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
                ApertureRatio = t.ApertureRatio,
                EyepieceFittingDiameter = t.EyepieceFittingDiameter,
                FocusDistance = t.FocusDistance,
                MaxUsefulScale = t.MaxUsefulScale,
                MinUsefulScale = t.MinUsefulScale,
                MountingType = t.MountingType,
                TelescopeControlType = t.TelescopeControlType,
                ScaleMax = t.ScaleMax,
                ScaleMin = t.ScaleMin,
                Seeker = t.Seeker,
                TripodHeight = t.TripodHeight,
                TripodMaterial = t.TripodMaterial,
                Type = t.Type,
                TelescopeUserLevel = t.TelescopeUserLevel,
                Weight = t.Weight,
                TelescopeEyepieces = t.TelescopeEyepieces,
                ProductImagesIds = t.Product.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .Select(f => f.FileId).ToList(),
            },
            t => t.ProductId == productId)
    {
    }
}