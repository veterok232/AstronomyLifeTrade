using ApplicationCore.Models.Files;
using MediatR;

namespace ApplicationCore.Handlers.File;

public class UploadFileCommand : IRequest
{
    public UploadFileCommand(ReadableFileModel file) => File = file;

    public ReadableFileModel File { get; }
}