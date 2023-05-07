using Api.Constants;
using Api.Controllers;
using Api.Extensions;
using ApplicationCore.Interfaces.Assignments;

namespace Api.Middleware;

/// <summary>
///     Middleware to check if user has chosen assignment.
/// </summary>
public class AssignmentGuardMiddleware
{
    private static readonly List<string> ActionsWhiteList = new List<string>
    {
        "/api/contexts",
        "/api/identity/refresh-access-token",
        "/api/identity/logout",
        "/api/identity/login",
    };

    private readonly RequestDelegate _next;

    private readonly IAssignmentService _assignmentService;

    public AssignmentGuardMiddleware(
        RequestDelegate next,
        IAssignmentService assignmentService)
    {
        _next = next;
        _assignmentService = assignmentService;
    }

    public async Task Invoke(HttpContext context)
    {
        /*if (!context.User.Identity.IsAuthenticated || context.IsCurrentAssignmentChosen())
        {
            await _next(context);
            return;
        }

        if (ActionsWhiteList.Contains(context.Request.Path.Value))
        {
            await _next(context);
            return;
        }

        if (await ShouldRejectIfAssignmentIsMissing(context))
        {
            context.Response.Headers.Add(CustomHeaders.AssignmentIsMissing, "true");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        if (await ShouldPreventUserToChooseAssignment(context))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied!");
            return;
        }

        await _next(context);*/
    }
}