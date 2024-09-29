using Core.Constants.Enums;

namespace Core.Services.Messages;

/// <summary>
/// Represents a base class for service messages.
/// </summary>
public abstract class ServiceMessage(string description, ServiceMessageType serviceMessageType)
{
    /// <summary>
    /// Gets or initializes the status of the service message.
    /// </summary>
    public string Status { get; init; } = serviceMessageType.ToString();

    /// <summary>
    /// Gets or initializes the description of the service message.
    /// </summary>
    public string Description { get; init; } = description;
}