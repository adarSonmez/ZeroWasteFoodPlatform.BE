using Newtonsoft.Json;

namespace Core.Services.Result;

/// <summary>
/// Represents a service result that contains an object of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data object.</typeparam>
public class ServiceObjectResult<T> : ServiceResult
{
    [JsonProperty]
    public bool HasData { get; private set; }

    [JsonProperty]
    public T? Data { get; private set; }

    /// <summary>
    /// Sets the data object and marks the result as successful.
    /// </summary>
    /// <param name="data">The data object to set.</param>
    /// <param name="message">The success message (optional).</param>
    public void SetData(T? data, string? message = null)
    {
        if (data == null)
        {
            Fail(ServiceResultConstants.NotFound);
        }
        else
        {
            Data = data;
            DataType = typeof(T).Name;
            HasData = true;

            Success(message ?? ServiceResultConstants.Success);
        }
    }

    #region Fail Overloads

    /// <inheritdoc/>
    public override void Fail()
    {
        base.Fail();
        Data = default;
        HasData = false;
    }

    /// <inheritdoc/>
    public override void Fail(string code, string description)
    {
        base.Fail(code, description);
        Data = default;
        HasData = false;
    }

    /// <inheritdoc/>
    public override void Fail(string description)
    {
        base.Fail(description);
        Data = default;
        HasData = false;
    }

    /// <inheritdoc/>
    public override void Fail(ServiceResult? result)
    {
        base.Fail(result);
        Data = default;
        HasData = false;
    }

    /// <inheritdoc/>
    public override void Fail(Exception ex)
    {
        base.Fail(ex);
        Data = default;
        HasData = false;
    }

    #endregion Fail Overloads
}