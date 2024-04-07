namespace Core.Extensions;

public static class GuidExtensions
{
    public static bool Same(this Guid guid, string value)
    {
        return guid.ToString().Equals(value);
    }
}