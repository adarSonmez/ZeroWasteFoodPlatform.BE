namespace Core.Constants.StringConstants;

/// <summary>
/// Contains constants for authentication policies.
/// </summary>
public static class AuthPolicies
{
    public const string AllowAll = "AllowAll";
    public const string AdminOnly = "AdminOnly";
    public const string CustomerOnly = "CustomerOnly";
    public const string BusinessOnly = "BusinessOnly";
    public const string AdminOrBusiness = "AdminOrBusiness";
    public const string AdminOrCustomer = "AdminOrCustomer";
}