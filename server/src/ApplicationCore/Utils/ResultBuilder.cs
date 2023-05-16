using ApplicationCore.Models.Common;

namespace ApplicationCore.Utils;

public static class ResultBuilder
{
    public static Result BuildSucceeded()
    {
        return new Result
        {
            IsSucceeded = true,
        };
    }

    public static Result<T> BuildSucceeded<T>()
    {
        return new Result<T>
        {
            IsSucceeded = true,
        };
    }

    public static Result<T> BuildSucceeded<T>(T data)
    {
        return new Result<T>
        {
            IsSucceeded = true,
            Data = data,
        };
    }

    public static Result BuildFailed(params string[] errors)
    {
        return new Result
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result<T> BuildFailed<T>(params string[] errors)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result BuildFailed(IEnumerable<string> errors)
    {
        return new Result
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result<T> BuildFailed<T>(IEnumerable<string> errors)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result<T> BuildFailedWithData<T>(T data, params string[] errors)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Data = data,
            Errors = errors,
        };
    }

    public static Result<T> BuildFailedWithData<T>(T data, IEnumerable<string> errors)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Data = data,
            Errors = errors,
        };
    }

    public static Result BuildFromResult(bool isSucceeded)
    {
        return new Result
        {
            IsSucceeded = isSucceeded,
        };
    }

    public static Result BuildFromResult(bool isSucceeded, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSucceeded = isSucceeded,
            Errors = errors,
        };
    }

    public static Result<T> BuildFromResult<T>(bool isSucceeded)
    {
        return new Result<T>
        {
            IsSucceeded = isSucceeded,
        };
    }

    public static Result RebuildData(Result result)
    {
        return new()
        {
            IsSucceeded = result.IsSucceeded,
            Errors = result.Errors,
        };
    }

    public static Result<T> RebuildData<T>(Result result, T data = default)
    {
        return new()
        {
            IsSucceeded = result.IsSucceeded,
            Errors = result.Errors,
            Data = data,
        };
    }

    public static Result<T> Build<T>(bool isSucceeded, IEnumerable<string> errors, T data)
    {
        return new()
        {
            IsSucceeded = isSucceeded,
            Data = data,
            Errors = errors,
        };
    }

    public static Result<T> Build<T>(bool isSucceeded, T data = default)
    {
        return new Result<T>
        {
            IsSucceeded = isSucceeded,
            Data = data,
        };
    }
}