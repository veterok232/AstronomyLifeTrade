using Api.Constants;
using ApplicationCore.Constants;

namespace Api.Extensions;

internal static class HttpRequestExtensions
{
    private const string AcceptHeaderName = "Accept";

    public static bool IsHtmlPageRequest(this HttpRequest request)
    {
        return request.Method == HttpMethods.Get &&
               request.Headers[AcceptHeaderName].Any(header =>
                   header.Contains(MimeTypes.Html, StringComparison.InvariantCulture));
    }

    public static bool IsSwaggerRequest(this HttpRequest request)
    {
        return request.Path.StartsWithSegments(Routes.Swagger, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IsHangfireRequest(this HttpRequest request)
    {
        return request.Path.StartsWithSegments(Routes.Hangfire, StringComparison.InvariantCultureIgnoreCase);
    }

    public static async Task<string> GetBodyAsString(this HttpRequest request)
    {
        using var reader = new StreamReader(request.Body);
        return await reader.ReadToEndAsync();
    }
}