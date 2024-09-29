using Core.Constants.StringConstants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Extensions;

/// <summary>
/// Extension methods for HttpContextAccessor.
/// </summary>
public static class HttpContextAccessorExtensions
{
    /// <summary>
    /// Gets the user ID from the HttpContext.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>The user ID as a nullable Guid.</returns>
    public static Guid? GetUserId(this HttpContext httpContext)
    {
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return Guid.TryParse(userId, out var result) ? result : null;
    }

    /// <summary>
    /// Gets the email from the HttpContext.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>The email as a nullable string.</returns>
    public static string? GetEmail(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }

    /// <summary>
    /// Checks if the user is an admin.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>True if the user is an admin, otherwise false.</returns>
    public static bool IsAdmin(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Admin;
    }

    /// <summary>
    /// Checks if the user is a customer.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>True if the user is a customer, otherwise false.</returns>
    public static bool IsCustomer(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Customer;
    }

    /// <summary>
    /// Checks if the user is a business.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>True if the user is a business, otherwise false.</returns>
    public static bool IsBusiness(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Business;
    }

    /// <summary>
    /// Gets the username from the HttpContext.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>The username as a nullable string.</returns>
    public static string? GetUsername(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// Gets the role from the HttpContext.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <returns>The role as a nullable string.</returns>
    public static string? GetRole(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    }
}