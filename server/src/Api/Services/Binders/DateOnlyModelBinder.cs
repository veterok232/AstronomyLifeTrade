using Api.Interfaces.Binders;
using ApplicationCore.Services.Dependencies.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Services.Binders;

[ScopedDependency]
internal class DateOnlyModelBinder : IDateOnlyModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
        bindingContext.Result = ModelBindingResult.Success(DateOnly.Parse(valueProviderResult.FirstValue));

        return Task.CompletedTask;
    }
}