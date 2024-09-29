using Core.Constants.Enums;

namespace Core.Services.Messages;

/// <summary>
/// Represents an error message in the service layer.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ErrorMessage"/> class with the specified code and description.
/// </remarks>
/// <param name="code">The error code.</param>
/// <param name="description">The error description.</param>
public class ErrorMessage(string code, string description) : ServiceMessage(description, ServiceMessageType.Error)
{
    public string ErrorCode { get; set; } = code;
}