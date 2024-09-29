using Core.Constants.Enums;

namespace Core.Services.Messages;

/// <summary>
/// Represents a success message that can be used in service operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SuccessMessage"/> class with the specified code and description.
/// </remarks>
/// <param name="description">The description of the success message.</param>
public class SuccessMessage(string description) : ServiceMessage(description, ServiceMessageType.Success)
{
}