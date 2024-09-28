namespace Core.Services.Messages;

/// <summary>
/// Represents a base class for service messages.
/// </summary>
public abstract class ServiceMessage(string? code, string? description)
{
    /// <summary>
    /// Gets or sets the code of the service message.
    /// </summary>
    public string? Code { get; set; } = code;

    /// <summary>
    /// Gets or sets the description of the service message.
    /// </summary>
    public string? Description { get; set; } = description;

    /// <summary>
    /// Gets or sets a value indicating whether the service message is a warning.
    /// </summary>
    public bool IsWarning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service message is a success.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service message is an error.
    /// </summary>
    public bool IsError { get; set; }
}