using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using MediatR;

namespace ApplicationCore.Handlers.File;

internal class FileContentQueryHandler : IRequestHandler<FileContentQuery, FileDescriptor>
{
    private readonly IFileStorageService _fileService;

    public FileContentQueryHandler(IFileStorageService fileService)
    {
        _fileService = fileService;
    }

    public Task<FileDescriptor> Handle(FileContentQuery contentQuery, CancellationToken cancellationToken)
    {
        return _fileService.GetOwned(contentQuery.FileId);
    }
}