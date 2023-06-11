using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

internal abstract class SearchProductsListBaseSpecification : DataTransformSpecification<Product, ProductListItem>
{
    protected SearchProductsListBaseSpecification(
        Expression<Func<Product, bool>> criteria)
        : base(
            t => new ProductListItem()
            {
                ProductId = t.Id,
                Brand = new BrandModel
                {
                    Id = t.BrandId,
                    Name = t.Brand.Name,
                },
                Category = new CategoryModel
                {
                    Code = t.Category.Code,
                    Description = t.Category.Description,
                    Id = t.CategoryId,
                    Name = t.Category.Name,
                    ProductsCount = t.Category.ProductsCount,
                },
                ProductRating = new ProductRatingModel
                {
                    CommentsCount = t.Comments != null
                        ? t.Comments.Count
                        : 0,
                    Rating = t.Comments != null
                        ? t.Comments.Select(c => c.Rating).Any()
                            ? t.Comments.Select(c => c.Rating).Average()
                            : 0
                        : 0,
                },
                Name = t.Name,
                Price = t.Price,
                Quantity = t.Quantity,
                ShortDescription = t.ShortDescription,
                SpecialNote = t.SpecialNote,
                Code = t.Code,
                ImageFilesIds = t.Files
                    .Where(f => f.ProductFileType == ProductFileType.Image)
                    .Select(f => f.FileId).ToList(),
            },
            criteria)
    {
        AddIncludes(
            t => t.Brand,
            t => t.Category,
            t => t.Comments,
            t => t.Files);
    }
}