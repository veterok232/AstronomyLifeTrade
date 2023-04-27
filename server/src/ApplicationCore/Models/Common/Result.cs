namespace ApplicationCore.Models.Common;

public class Result
{
    public bool IsSucceeded { get; init; }

    public IEnumerable<string> Errors { get; init; }
}