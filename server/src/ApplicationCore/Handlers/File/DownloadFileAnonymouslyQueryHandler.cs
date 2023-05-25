using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using MediatR;

namespace ApplicationCore.Handlers.File;

internal class DownloadFileAnonymouslyQueryHandler : IRequestHandler<DownloadFileAnonymouslyQuery, FileDescriptor>
{
    private readonly IFileAnonymousDownloadService _fileAnonymousDownloadService;

    public DownloadFileAnonymouslyQueryHandler(IFileAnonymousDownloadService fileAnonymousDownloadService)
    {
        _fileAnonymousDownloadService = fileAnonymousDownloadService;
    }

    public Task<FileDescriptor> Handle(DownloadFileAnonymouslyQuery query, CancellationToken cancellationToken)
    {
        return _fileAnonymousDownloadService.Download(query.FileId);
    }
}