namespace Api.Extensions;

internal static class RouterExtensions
{
    public static (string ControllerName, string ActionName) GetActionInfo(this HttpContext httpContext)
    {
        RouteData routeData = httpContext.GetRouteData();
        var controllerName = $"{routeData.Values["controller"]}Controller";
        var actionName = routeData.Values["action"].ToString();

        return (controllerName, actionName);
    }
}