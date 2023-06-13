using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

internal abstract class BinocularsListBaseSpecification : DataTransformSpecification<Binocular, ProductListItem>
{
    protected BinocularsListBaseSpecification(
        Expression<Func<Binocular, bool>> criteria)
        : base(
            t => new ProductListItem()
            {
                ProductId = t.ProductId,
                Brand = new BrandModel
                {
                    Id = t.Product.BrandId,
                    Name = t.Product.Brand.Name,
                },
                Category = new CategoryModel
                {
                    Code = t.Product.Category.Code,
                    Description = t.Product.Category.Description,
                    Id = t.Product.CategoryId,
                    Name = t.Product.Category.Name,
                    ProductsCount = t.Product.Category.ProductsCount,
                },
                CharacteristicsModels = new List<CharacteristicModel>
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
                Name = t.Product.Name,
                Price = t.Product.Price,
                Quantity = t.Product.Quantity,
                ShortDescription = t.Product.ShortDescription,
                SpecialNote = t.Product.SpecialNote,
                Code = t.Product.Code,
                ImageFilesIds = t.Product.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .OrderBy(pf => pf.File.FileName)
                    .Select(f => f.FileId).ToList(),
            },
            criteria)
    {
        AddIncludes(
            t => t.Product,
            t => t.Product.Brand,
            t => t.Product.Category,
            t => t.Product.Comments,
            t => t.Product.Files);
    }
}