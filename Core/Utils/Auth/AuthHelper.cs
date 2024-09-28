using Core.Extensions;
using Core.Utils.IoC;
using Microsoft.AspNetCore.Http;

namespace Core.Utils.Auth;

/// <summary>
/// Helper class for authentication-related operations.
/// </summary>
public static class AuthHelper
{
    private static readonly IHttpContextAccessor HttpContextAccessor = ServiceTool.GetService<IHttpContextAccessor>()!;

    /// <summary>
    /// Gets the user ID from the current HTTP context.
    /// </summary>
    /// <returns>The user ID, or null if not available.</returns>
    public static Guid? GetUserId()
    {
        return HttpContextAccessor.HttpContext!.GetUserId();
    }

    /// <summary>
    /// Gets the email address from the current HTTP context.
    /// </summary>
    /// <returns>The email address, or null if not available.</returns>
    public static string? GetEmail()
    {
        return HttpContextAccessor.HttpContext!.GetEmail();
    }

    /// <summary>
    /// Gets the username from the current HTTP context.
    /// </summary>
    /// <returns>The username, or null if not available.</returns>
    public static string? GetUsername()
    {
        return HttpContextAccessor.HttpContext!.GetUsername();
    }

    /// <summary>
    /// Checks if the user is logged in as an admin.
    /// </summary>
    /// <returns>True if the user is logged in as an admin, false otherwise.</returns>
    public static bool IsLoggedInAsAdmin()
    {
        return HttpContextAccessor.HttpContext!.IsAdmin();
    }

    /// <summary>
    /// Checks if the user is logged in as a customer.
    /// </summary>
    /// <returns>True if the user is logged in as a customer, false otherwise.</returns>
    public static bool IsLoggedInAsCustomer()
    {
        return HttpContextAccessor.HttpContext!.IsCustomer();
    }

    /// <summary>
    /// Checks if the user is logged in as a business.
    /// </summary>
    /// <returns>True if the user is logged in as a business, false otherwise.</returns>
    public static bool IsLoggedInAsBusiness()
    {
        return HttpContextAccessor.HttpContext!.IsBusiness();
    }

    /// <summary>
    /// Gets the role from the current HTTP context.
    /// </summary>
    /// <returns>The role, or null if not available.</returns>
    public static string? GetRole()
    {
        return HttpContextAccessor.HttpContext!.GetRole();
    }
}