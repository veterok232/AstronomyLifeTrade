using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Controllers.Filters;

public class LocalizationFilter : IResourceFilter
{
    private const string HeaderAcceptLanguageKey = "Accept-Language";

    private static readonly Regex PrimaryLanguageRegex = new Regex("^\\S+?(?=[,;\\s])");

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        SetThreadCultureFromRequest(context.HttpContext.Request);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // No Action Required
    }

    private static void SetThreadCultureFromRequest(HttpRequest request)
    {
        var acceptLanguageHeader = request.Headers[HeaderAcceptLanguageKey].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(acceptLanguageHeader))
        {
            return;
        }

        var requestLanguage = PrimaryLanguageRegex.Match(acceptLanguageHeader).Value;
        if (!string.IsNullOrEmpty(requestLanguage))
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(requestLanguage);
        }
    }
}