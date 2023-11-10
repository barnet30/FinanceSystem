namespace FinanceSystem.Abstractions.Models.Result;

/// <summary>
/// Enum for error types
/// </summary>
public enum OperationErrorType
{
    /// <summary>
    /// Validation error
    /// </summary>
    Validation = 400,
    
    /// <summary>
    /// Not found error
    /// </summary>
    NotFound = 404,
    
    /// <summary>
    /// Forbidden error
    /// </summary>
    Forbidden = 403,
    
    /// <summary>
    /// Unauthorized error
    /// </summary>
    Unauthorized = 401,
    
    /// <summary>
    /// Internal server error
    /// </summary>
    Internal = 500
}

/// <summary>
/// Extension to convert error types
/// </summary>
public static class OperationErrorTypeExtensions
{
    /// <summary>
    /// Return description of error type
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToDescription(this OperationErrorType value) => value switch
    {
        OperationErrorType.Validation => "Validation error",
        OperationErrorType.NotFound => "Not found",
        OperationErrorType.Forbidden => "Forbidden",
        OperationErrorType.Internal => "Internal server error",
        OperationErrorType.Unauthorized => "Unauthorized",
        _ => "Unknown error"
    };

    /// <summary>
    /// Return key of error
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToType(this OperationErrorType value) => value switch
    {
        OperationErrorType.Validation => DefaultErrorKeys.ValidationError,
        OperationErrorType.NotFound => DefaultErrorKeys.NotFound,
        OperationErrorType.Forbidden => DefaultErrorKeys.Forbidden,
        OperationErrorType.Unauthorized => DefaultErrorKeys.Unauthorized,
        OperationErrorType.Internal => DefaultErrorKeys.Internal,
        _ => string.Empty
    };
}