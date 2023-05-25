namespace ApplicationCore.Interfaces.Files;

internal interface ISaveFileValidator
{
    bool IsInputDataValid(Stream stream);
}