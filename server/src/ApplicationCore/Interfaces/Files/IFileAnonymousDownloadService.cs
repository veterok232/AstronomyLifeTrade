using ApplicationCore.Models.Files;

namespace ApplicationCore.Interfaces.Files;

internal interface IFileAnonymousDownloadService
{
    Task<FileDescriptor> Download(Guid fileId);
}