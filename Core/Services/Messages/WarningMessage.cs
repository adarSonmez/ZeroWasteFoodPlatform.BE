namespace Core.Services.Messages;

/// <summary>
/// Represents a warning message in the service.
/// </summary>
public class WarningMessage : ServiceMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarningMessage"/> class with the specified code and description.
    /// </summary>
    /// <param name="code">The code of the warning message.</param>
    /// <param name="description">The description of the warning message.</param>
    public WarningMessage(string? code, string? description) : base(code, description)
    {
        IsWarning = true;
    }
}