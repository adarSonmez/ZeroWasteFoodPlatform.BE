using Core.Extensions;
using Core.Utils.IoC;
using Microsoft.AspNetCore.Http;

namespace Core.Utils.Auth;

public static class AuthHelper
{
    private static readonly IHttpContextAccessor HttpContextAccessor = ServiceTool.GetService<IHttpContextAccessor>()!;

    public static string? GetUserId()
    {
        return HttpContextAccessor.HttpContext!.GetUserId();
    }

    public static string? GetEmail()
    {
        return HttpContextAccessor.HttpContext!.GetEmail();
    }

    public static string? GetUsername()
    {
        return HttpContextAccessor.HttpContext!.GetUsername();
    }

    public static bool IsLoggedInAsAdmin()
    {
        return HttpContextAccessor.HttpContext!.IsAdmin();
    }

    public static bool IsLoggedInAsCustomer()
    {
        return HttpContextAccessor.HttpContext!.IsCustomer();
    }

    public static bool IsLoggedInAsBusiness()
    {
        return HttpContextAccessor.HttpContext!.IsBusiness();
    }

    public static string? GetRole()
    {
        return HttpContextAccessor.HttpContext!.GetRole();
    }
}