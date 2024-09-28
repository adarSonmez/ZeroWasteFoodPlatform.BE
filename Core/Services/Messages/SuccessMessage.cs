namespace Core.Services.Messages;

/// <summary>
/// Represents a success message that can be used in service operations.
/// </summary>
public class SuccessMessage : ServiceMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessMessage"/> class with the specified code and description.
    /// </summary>
    /// <param name="code">The code associated with the success message.</param>
    /// <param name="description">The description of the success message.</param>
    public SuccessMessage(string? code, string? description) : base(code, description)
    {
        IsSuccess = true;
    }
}