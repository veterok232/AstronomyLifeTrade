using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

internal abstract class PopularProductsBaseSpecification : DataTransformSpecification<Product, ProductListItem>
{
    protected PopularProductsBaseSpecification(
        Expression<Func<Product, bool>> criteria)
        : base(
            p => new ProductListItem()
            {
                ProductId = p.Id,
                Brand = new BrandModel
                {
                    Id = p.BrandId,
                    Name = p.Brand.Name,
                },
                Category = new CategoryModel
                {
                    Code = p.Category.Code,
                    Description = p.Category.Description,
                    Id = p.CategoryId,
                    Name = p.Category.Name,
                    ProductsCount = p.Category.ProductsCount,
                },
                ProductRating = new ProductRatingModel
                {
                    CommentsCount = p.Comments != null
                        ? p.Comments.Count
                        : 0,
                    Rating = p.Comments != null
                        ? p.Comments.Select(c => c.Rating).Any()
                            ? p.Comments.Select(c => c.Rating).Average()
                            : 0
                        : 0,
                },
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ShortDescription = p.ShortDescription,
                SpecialNote = p.SpecialNote,
                Code = p.Code,
                ImageFilesIds = p.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .Select(f => f.FileId).ToList(),
            },
            criteria)
    {
        AddIncludes(
            p => p.Brand,
            p => p.Category,
            p => p.Comments,
            p => p.Files);
    }
}