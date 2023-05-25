using ApplicationCore.Models.Files;
using MediatR;

namespace ApplicationCore.Handlers.File;

public class FileContentQuery : IRequest<FileDescriptor>
{
    public FileContentQuery(Guid fileId) => FileId = fileId;

    public Guid FileId { get; set; }
}