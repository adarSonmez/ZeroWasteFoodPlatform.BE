using Core.ExceptionHandling;
using Core.Services.Messages;
using Core.Services.Payload;

namespace Core.Services.Result;

/// <summary>
/// Represents the base class for service results.
/// </summary>
public abstract class ServiceResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the service operation has failed.
    /// </summary>
    public bool HasFailed { get; set; }

    /// <summary>
    /// Gets or sets the list of service messages.
    /// </summary>
    public IList<ServiceMessage> Messages { get; set; } = new List<ServiceMessage>();

    /// <summary>
    /// Gets or sets the list of extra data associated with the service result.
    /// </summary>
    public IList<ServicePayloadItem> ExtraData { get; set; } = new List<ServicePayloadItem>();

    /// <summary>
    /// Gets or sets the result code associated with the service result.
    /// </summary>
    public string? ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the data type of the service result.
    /// </summary>
    public string? DataType { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service result is a list.
    /// </summary>
    public bool IsList { get; protected set; }

    #region Success

    /// <summary>
    /// Adds a success message with the default success code to the service result.
    /// </summary>
    /// <param name="description">The success description.</param>
    public void Success(string description)
    {
        Success("S", description);
    }

    /// <summary>
    /// Adds a success message to the service result.
    /// </summary>
    /// <param name="code">The success code.</param>
    /// <param name="description">The success description.</param>
    private void Success(string code, string description)
    {
        Messages.Add(new SuccessMessage(code, description));
    }

    #endregion Success

    #region Warning

    /// <summary>
    /// Adds a warning message with the default warning code to the service result.
    /// </summary>
    /// <param name="description">The warning description.</param>
    public void Warning(string description)
    {
        Warning("W", description);
    }

    /// <summary>
    /// Adds a warning message to the service result.
    /// </summary>
    /// <param name="code">The warning code.</param>
    /// <param name="description">The warning description.</param>
    private void Warning(string code, string description)
    {
        Messages.Add(new WarningMessage(code, description));
    }

    #endregion Warning

    #region Error

    /// <summary>
    /// Marks the service operation as failed.
    /// </summary>
    public virtual void Fail()
    {
        HasFailed = true;
    }

    /// <summary>
    /// Adds a service message to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="message">The service message.</param>
    public virtual void Fail(ServiceMessage message)
    {
        Messages.Add(message);
        Fail();
    }

    /// <summary>
    /// Adds an error message to the service result with the specified error code and description, and marks the service operation as failed.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public virtual void Fail(string code, string description)
    {
        Fail(new ErrorMessage(code, description));
    }

    /// <summary>
    /// Adds an error message to the service result with the default error code and the specified error description, and marks the service operation as failed.
    /// </summary>
    /// <param name="description">The error description.</param>
    public virtual void Fail(string description)
    {
        Fail("E", description);
    }

    /// <summary>
    /// Adds a list of service messages to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="messages">The list of service messages.</param>
    public virtual void Fail(IList<ServiceMessage>? messages)
    {
        Fail(messages?.AsEnumerable());
    }

    /// <summary>
    /// Adds a collection of service messages to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="messages">The collection of service messages.</param>
    public virtual void Fail(IEnumerable<ServiceMessage>? messages)
    {
        if (messages == null) return;
        var serviceMessages = messages.ToList();
        if (serviceMessages.Count == 0) return;
        foreach (var message in serviceMessages)
            Fail(message);
    }

    /// <summary>
    /// Adds a collection of collections of service messages to the service result and marks the service operation as failed.
    /// </summary>
    /// <param name="messages">The collection of collections of service messages.</param>
    public void Fail(IEnumerable<IEnumerable<ServiceMessage>>? messages)
    {
        if (messages == null) return;

        var messagesEnumerable = messages.ToList();
        if (messagesEnumerable.Count == 0) return;
        foreach (var message in messagesEnumerable) Fail(message);
    }

    /// <summary>
    /// Adds the service messages from another service result to the current service result and marks the service operation as failed.
    /// </summary>
    /// <param name="result">The other service result.</param>
    public virtual void Fail(ServiceResult? result)
    {
        Fail(result?.Messages);
    }

    /// <summary>
    /// Adds an error message to the service result based on the specified exception and marks the service operation as failed.
    /// </summary>
    /// <param name="ex">The exception.</param>
    public virtual void Fail(Exception ex)
    {
        switch (ex)
        {
            case ValidationException validationException:
                Fail(validationException);
                break;

            default:
                Fail(ex.Message);
                break;
        }
    }

    /// <summary>
    /// Adds an error message to the service result based on the specified validation exception and marks the service operation as failed.
    /// </summary>
    /// <param name="validationException">The validation exception.</param>
    private void Fail(ValidationException validationException)
    {
        Fail(validationException.ExceptionCode, validationException.Message);
    }

    #endregion Error
}