using ApplicationCore.Interfaces.DataAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Controllers.Filters;

internal class DataAuthorizationFilter : IActionFilter
{
    private readonly IDataAuthorizationService _dataAuthorizationService;

    public DataAuthorizationFilter(IDataAuthorizationService dataAuthorizationService)
    {
        _dataAuthorizationService = dataAuthorizationService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // do nothing
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = context.Result;
        if (result is not ObjectResult objectResult || objectResult.Value == null)
        {
            return;
        }

        _dataAuthorizationService.Authorize(objectResult.Value);
    }
}