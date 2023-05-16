using ApplicationCore.Extensions;

namespace ApplicationCore.Models.Common;
public class ValidationResult
{
    private ValidationResult()
    {
    }

    public bool IsValid => Errors.IsNullOrEmpty();

    public IEnumerable<string> Errors { get; private set; }

    public static ValidationResult CreateValid()
    {
        return new ValidationResult();
    }

    public static ValidationResult CreateInvalid(params string[] errors)
    {
        return CreateFromErrors(errors);
    }

    public static ValidationResult CreateFromErrors(IEnumerable<string> errors)
    {
        return new ValidationResult { Errors = errors };
    }
}