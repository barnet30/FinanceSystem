namespace FinanceSystem.Abstractions.Models.Result;

/// <summary>
/// Errors of result
/// </summary>
public class ResultErrors
{
    /// <summary>
    /// Error type
    /// </summary>
    public OperationErrorType Type { get; }
    
    /// <summary>
    /// Dictionary of errors
    /// </summary>
    public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type"></param>
    public ResultErrors(OperationErrorType type)
    {
        Type = type;
    }
}