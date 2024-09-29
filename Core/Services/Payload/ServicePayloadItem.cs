namespace Core.Services.Payload;

/// <summary>
/// Represents a key-value pair in a service payload.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ServicePayloadItem"/> class with the specified key and value.
/// </remarks>
/// <param name="key">The key of the payload item.</param>
/// <param name="value">The value of the payload item.</param>
public class ServicePayloadItem(string key, object value)
{
    /// <summary>
    /// Gets or sets the key of the payload item.
    /// </summary>
    public string Key { get; set; } = key;

    /// <summary>
    /// Gets or sets the value of the payload item.
    /// </summary>
    public object Value { get; set; } = value;
}