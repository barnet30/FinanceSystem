namespace FinanceSystem.Abstractions.Models.Result;

/// <summary>
/// Result class for different operations 
/// </summary>
public class Result
{
    /// <summary>
    /// Errors of result of operation
    /// </summary>
    public ResultErrors Errors { get; }
    
    /// <summary>
    /// Check if result of operation is success
    /// </summary>
    public bool IsSuccess => Errors == null || Errors.Values.Count == 0;

    /// <summary>
    /// Constructor for creating result of operation with specified error type
    /// </summary>
    /// <param name="errorType">Error type</param>
    /// <param name="errors">Dictionary of errors</param>
    protected Result(OperationErrorType errorType, IDictionary<string, string> errors)
    {
        Errors = new ResultErrors(errorType) { Values = errors };
    }
    
    /// <summary>
    /// Constructor for creating result of operation with specified errors
    /// </summary>
    /// <param name="errors">Errors</param>
    protected Result(ResultErrors errors)
    {
        Errors = errors;
    }
    
    /// <summary>
    /// Contructor for creating success result
    /// </summary>
    protected Result()
    { }
    
    /// <summary>
    /// Return success result
    /// </summary>
    public static Result Success => new();

    /// <summary>
    /// Return result by errors
    /// </summary>
    /// <param name="resultErrors">Errors</param>
    /// <returns></returns>
    public static Result FromErrors(ResultErrors resultErrors) => new(resultErrors);

    /// <summary>
    /// Returns not found error result
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="key">Error key</param>
    /// <returns></returns>
    public static Result NotFound(string errorMessage, string key = DefaultErrorKeys.NotFound) =>
        new(OperationErrorType.NotFound, new Dictionary<string, string>
        {
            { key, errorMessage }
        });
    
    /// <summary>
    /// Returns not valid error result
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="key">Error key</param>
    /// <returns></returns>
    public static Result NotValid(string errorMessage, string key = DefaultErrorKeys.ValidationError) =>
        new(OperationErrorType.Validation, new Dictionary<string, string>
        {
            { key, errorMessage }
        });
    
    /// <summary>
    /// Returns internal server error result
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="key">Error key</param>
    /// <returns></returns>
    public static Result Internal(string errorMessage, string key = DefaultErrorKeys.Internal) =>
        new(OperationErrorType.Internal, new Dictionary<string, string>
        {
            { key, errorMessage }
        });
    
    /// <summary>
    /// Returns forbidden error result
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="key">Error key</param>
    /// <returns></returns>
    public static Result Forbidden(string errorMessage, string key = DefaultErrorKeys.Forbidden) =>
        new(OperationErrorType.Forbidden, new Dictionary<string, string>
        {
            { key, errorMessage }
        });
    
    /// <summary>
    /// Returns unauthorized error result
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="key">Error key</param>
    /// <returns></returns>
    public static Result Unauthorized(string errorMessage, string key = DefaultErrorKeys.Unauthorized) =>
        new(OperationErrorType.Unauthorized, new Dictionary<string, string>
        {
            { key, errorMessage }
        });
}