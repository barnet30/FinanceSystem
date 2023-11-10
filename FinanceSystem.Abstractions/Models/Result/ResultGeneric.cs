namespace FinanceSystem.Abstractions.Models.Result;

/// <summary>
/// Result of operation with T data
/// </summary>
/// <typeparam name="T">Type of data</typeparam>
public sealed class Result<T> : Result
{
    /// <summary>
    /// Result data of operation
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// Success result
    /// </summary>
    /// <param name="data">Operation data</param>
    private Result(T data)
    {
        Data = data;
    }
    
    /// <summary>
    /// Result with errors
    /// </summary>
    /// <param name="errors">Errors.</param>
    private Result(ResultErrors errors) : base(errors)
    {
    }

    /// <summary>
    /// Creates success result with data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<T> FromValue(T data) => new(data);
    
    /// <inheritdoc cref="Result.FromErrors"/>
    public new static Result<T> FromErrors(ResultErrors errors) => new(errors);

    /// <inheritdoc cref="Result.NotFound"/>
    public new static Result<T> NotFound(string errorMessage, string key = DefaultErrorKeys.NotFound) =>
        FromErrors(Result.NotFound(errorMessage, key).Errors);

    /// <inheritdoc cref="Result.Forbidden"/>
    public new static Result<T> Forbidden(string errorMessage, string key = DefaultErrorKeys.Forbidden) =>
        FromErrors(Result.Forbidden(errorMessage, key).Errors);

    /// <inheritdoc cref="Result.Internal"/>
    public new static Result<T> Internal(string errorMessage, string key = DefaultErrorKeys.Internal) =>
        FromErrors(Result.Internal(errorMessage, key).Errors);
    
    /// <inheritdoc cref="Result.Unauthorized"/>
    public new static Result<T> Unauthorized(string errorMessage, string key = DefaultErrorKeys.Unauthorized) =>
        FromErrors(Result.Unauthorized(errorMessage, key).Errors);
    /// <inheritdoc cref="Result.NotValid"/>
    public new static Result<T> NotValid(string errorMessage, string key = DefaultErrorKeys.ValidationError) =>
        FromErrors(Result.NotValid(errorMessage, key).Errors);
}