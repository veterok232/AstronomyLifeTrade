using Api.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Attributes;

internal sealed class ApiRouteAttribute : RouteAttribute
{
    public ApiRouteAttribute()
        : this("[controller]")
    {
    }

    public ApiRouteAttribute(string route)
        : base($"{Routes.Api}/{route}")
    {
    }
}