using ApplicationCore.Models.Files;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

internal static class ControllerBaseExtensions
{
    public static FileStreamResult ConvertToFileStreamResult(
        this ControllerBase controller,
        FileDescriptor fileDescriptor)
    {
        return controller.File(fileDescriptor.Stream, fileDescriptor.MimeType, fileDescriptor.FileName);
    }
}