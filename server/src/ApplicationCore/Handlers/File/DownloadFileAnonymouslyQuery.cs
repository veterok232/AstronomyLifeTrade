using ApplicationCore.Models.Files;
using MediatR;

namespace ApplicationCore.Handlers.File;

public record DownloadFileAnonymouslyQuery(Guid FileId) : IRequest<FileDescriptor>;