using ApplicationCore.Interfaces.Files;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class SaveFileValidator : ISaveFileValidator
{
    public bool IsInputDataValid(Stream stream)
    {
        return stream.Length != 0;
    }
}