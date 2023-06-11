using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

internal abstract class AccessoriesListBaseSpecification : DataTransformSpecification<Accessory, ProductListItem>
{
    protected AccessoriesListBaseSpecification(
        Expression<Func<Accessory, bool>> criteria)
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
                CharacteristicsModels = new List<CharacteristicModel>(),
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