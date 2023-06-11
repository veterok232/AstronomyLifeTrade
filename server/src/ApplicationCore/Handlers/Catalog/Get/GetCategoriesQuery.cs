using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.Get;

public record GetCategoriesQuery : IRequest<ICollection<CategoryModel>>;