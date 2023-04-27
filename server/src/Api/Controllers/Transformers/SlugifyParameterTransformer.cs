using CaseExtensions;

namespace Api.Controllers.Transformers;

internal class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value) => value?.ToString().ToKebabCase();
}