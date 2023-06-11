using ApplicationCore.Handlers.Common;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Catalog.Search;
using MediatR;

namespace ApplicationCore.Handlers.Catalog.SearchBinoculars;

public record SearchBinocularsRequest(BinocularSearchModel Model) : IRequest<SearchResult<ProductListItem>>;