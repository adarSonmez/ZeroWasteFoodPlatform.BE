using Core.Constants.Enums;

namespace Core.Services.Messages;

/// <summary>
/// Represents a warning message in the service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WarningMessage"/> class with the specified code and description.
/// </remarks>
/// <param name="description">The description of the warning message.</param>
public class WarningMessage(string description) : ServiceMessage(description, ServiceMessageType.Warning)
{
}