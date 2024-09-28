using Core.Constants;

namespace Core.ExceptionHandling;

/// <summary>
/// Represents an exception that occurs during validation.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ValidationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error code and message.
    /// </summary>
    /// <param name="code">The error code associated with the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ValidationException(string code, string message) : base(message)
    {
        ExceptionCode = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public ValidationException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Gets or sets the exception code associated with the exception.
    /// </summary>
    public string ExceptionCode { get; set; } = ValidationConstants.DefaultValidationExceptionCode;
}