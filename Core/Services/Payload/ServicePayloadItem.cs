namespace Core.Services.Payload;

/// <summary>
/// Represents a key-value pair in a service payload.
/// </summary>
public class ServicePayloadItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServicePayloadItem"/> class with the specified key and value.
    /// </summary>
    /// <param name="key">The key of the payload item.</param>
    /// <param name="value">The value of the payload item.</param>
    public ServicePayloadItem(string key, object value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the key of the payload item.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the value of the payload item.
    /// </summary>
    public object Value { get; set; }
}