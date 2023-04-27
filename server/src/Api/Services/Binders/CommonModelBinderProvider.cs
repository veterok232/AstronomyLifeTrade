using Api.Interfaces.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Services.Binders;

internal class CommonModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        return context?.Metadata.ModelType switch
        {
            { } t when t == typeof(DateOnly) => context.Services.GetService<IDateOnlyModelBinder>(),
            { } t when t == typeof(DateOnly?) => context.Services.GetService<IDateOnlyModelBinder>(),
            { } t when t == typeof(string) => context.Services.GetService<IStringTrimmingModelBinder>(),
            _ => null,
        };
    }
}