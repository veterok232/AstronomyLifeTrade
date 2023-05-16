namespace Infrastructure.Utils;

internal static class DbSequenceUtils
{
    public static string CreateSequenceFormula(string sequenceName) =>
        $"nextval('\"{sequenceName}\"')";
}