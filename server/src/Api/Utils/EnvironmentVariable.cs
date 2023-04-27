using Api.Constants;

namespace Api.Utils;

public static class EnvironmentVariable
{
    public static readonly bool IsDev = Environment
        .GetEnvironmentVariable(EnvironmentVariableNames.EnvironmentType) == Environments.Development;
}