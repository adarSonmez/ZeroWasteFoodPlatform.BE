namespace Core.Services.Messages;

/// <summary>
/// Represents an error message in the service layer.
/// </summary>
public class ErrorMessage : ServiceMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorMessage"/> class with the specified code and description.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public ErrorMessage(string? code, string? description) : base(code, description)
    {
        IsError = true;
    }
}