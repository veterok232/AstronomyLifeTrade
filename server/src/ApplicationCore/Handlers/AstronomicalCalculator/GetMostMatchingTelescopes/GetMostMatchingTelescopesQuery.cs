using ApplicationCore.Models.AstronomicalCalculator;
using ApplicationCore.Models.Catalog;
using MediatR;

namespace ApplicationCore.Handlers.AstronomicalCalculator.GetMostMatchingTelescopes;

public record GetMostMatchingTelescopesQuery(AstronomicalCalculatorMostMatchingModel Model)
    : IRequest<ICollection<ProductListItem>>;