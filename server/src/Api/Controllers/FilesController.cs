using Api.Constants;
using Api.Controllers.Attributes;
using Api.Extensions;
using ApplicationCore.Constants;
using ApplicationCore.Handlers.File;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiRoute]
[Authorize]
public class FilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.File.Download)]
    [Authorization(Roles.Staff, Roles.Manager, Roles.Consumer)]
    public async Task<FileStreamResult> Download(Guid fileId)
    {
        var fileDescriptor = await _mediator.Send(new FileContentQuery(fileId));

        return this.ConvertToFileStreamResult(fileDescriptor);
    }

    [HttpGet(Routes.File.DownloadAnonymously)]
    [AllowAnonymous]
    public async Task<FileStreamResult> DownloadAnonymously(Guid fileId)
    {
        var fileDescriptor = await _mediator.Send(new DownloadFileAnonymouslyQuery(fileId));

        return this.ConvertToFileStreamResult(fileDescriptor);
    }
}