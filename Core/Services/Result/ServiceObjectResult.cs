namespace Core.Services.Result;

/// <summary>
/// Represents a service result that contains an object of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data object.</typeparam>
public class ServiceObjectResult<T> : ServiceResult
{
    public T? Data { get; private set; }

    /// <summary>
    /// Sets the data object and marks the result as successful.
    /// If the data object is null, the result is marked as failed.
    /// </summary>
    /// <param name="data">The data object to set.</param>
    /// <param name="message">The success message (optional).</param>
    public void SetData(T? data, string message)
    {
        if (data == null)
        {
            Fail(ServiceResultConstants.NotFoundErrorCode, ServiceResultConstants.NotFound);
        }
        else
        {
            Data = data;
            DataType = typeof(T).Name;
            HasData = true;
            Success(message);
        }
    }
}