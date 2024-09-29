using Core.ExceptionHandling;
using Core.Services.Messages;
using Core.Services.Payload;

namespace Core.Services.Result;

/// <summary>
/// Represents the base class for service results.
/// </summary>
public abstract class ServiceResult
{
    #region Common Properties

    /// <summary>
    /// Gets or sets a value indicating whether the service operation has failed.
    /// </summary>
    public bool HasFailed { get; set; }

    /// <summary>
    /// Gets or sets the list of service messages.
    /// </summary>
    public IList<ServiceMessage> Messages { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of extra data associated with the service result.
    /// </summary>
    public IList<ServicePayloadItem> ExtraData { get; set; } = [];

    /// <summary>
    /// Gets or sets the data type of the service result.
    /// </summary>
    public string? DataType { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service result is a list.
    /// </summary>
    public bool IsList { get; protected set; }

    /// <summary>
    /// Gets a value indicating whether the service result has data.
    /// </summary>
    public bool HasData { get; protected set; }

    #endregion Common Properties

    #region Success

    /// <summary>
    /// Adds a success message with the default success code to the service result.
    /// </summary>
    /// <param name="description">The success description.</param>
    public void Success(string description)
    {
        Messages.Add(new SuccessMessage(description));
    }

    #endregion Success

    #region Warning

    /// <summary>
    /// Adds a warning message with the default warning code to the service result.
    /// </summary>
    /// <param name="description">The warning description.</param>
    public void Warning(string description)
    {
        Messages.Add(new WarningMessage(description));
    }

    #endregion Warning

    #region Error

    /// <summary>
    /// Adds a service message to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="message">The service message.</param>
    public void Fail(ServiceMessage message)
    {
        Messages.Add(message);
        HasFailed = true;
        HasData = false;
    }

    /// <summary>
    /// Adds an error message to the service result with the specified error code and description, and marks the service operation as failed.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public void Fail(string code, string description)
    {
        Fail(new ErrorMessage(code, description));
    }

    /// <summary>
    /// Adds a collection of service messages to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="messages">The collection of service messages.</param>
    public void Fail(IEnumerable<ServiceMessage>? messages)
    {
        if (messages == null || !messages.Any())
            return;

        foreach (var message in messages)
            Fail(message);
    }

    /// <summary>
    /// Adds the service messages from another service result to the current service result and marks the service operation as failed.
    /// </summary>
    /// <param name="result">The other service result.</param>
    public void Fail(ServiceResult? result)
    {
        Fail(result?.Messages);
    }

    /// <summary>
    /// Adds an error message to the service result based on the specified validation exception and marks the service operation as failed.
    /// </summary>
    /// <param name="validationException">The validation exception.</param>
    public void Fail(ValidationException validationException)
    {
        Fail(validationException.ExceptionCode, validationException.Message);
    }

    #endregion Error
}