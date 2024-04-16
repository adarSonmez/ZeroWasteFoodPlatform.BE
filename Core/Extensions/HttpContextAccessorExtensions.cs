using System.Security.Claims;
using Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions;

public static class HttpContextAccessorExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? GetEmail(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }

    public static bool IsAdmin(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Admin;
    }

    public static bool IsCustomer(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Customer;
    }

    public static bool IsBusiness(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == UserRoles.Business;
    }

    public static string? GetUsername(this HttpContext httpContext)
    {
        return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}