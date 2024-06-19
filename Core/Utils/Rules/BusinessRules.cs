using Core.Domain.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Result;
using Core.Utils.Auth;

namespace Core.Utils.Rules;

public static class BusinessRules
{
    public static void Run(params (string errCode, string? msg)[] checkResults)
    {
        foreach (var (errCode, msg) in checkResults)
            if (!string.IsNullOrEmpty(msg))
                throw new ValidationException(errCode, msg);
    }

    private static string? CheckEntityNull<TEntity>(TEntity? obj, string customError)
        where TEntity : IEntity
    {
        if (obj is not null) return null;

        if (string.IsNullOrEmpty(customError))
            customError = BusinessRulesMessages.NullObjectPassed;

        return customError;
    }

    public static string? CheckEntityNull<TEntity>(TEntity? obj)
        where TEntity : IEntity
    {
        return CheckEntityNull(obj, $"{typeof(TEntity).Name} Not Found!");
    }

    public static string? CheckDtoNull<TDto>(TDto? obj, string customError = BusinessRulesMessages.NullObjectPassed)
        where TDto : IDto
    {
        if (obj is not null) return null;

        if (string.IsNullOrEmpty(customError))
            customError = BusinessRulesMessages.NullObjectPassed;

        return customError;
    }

    public static string? CheckEmail(string email)
    {
        return !email.IsValidEmail() ? BusinessRulesMessages.EmailFormatIsNotValid : null;
    }

    public static string? CheckStringNullOrEmpty(string? str, string? customError = null)
    {
        if (string.IsNullOrEmpty(str))
            return string.IsNullOrEmpty(customError) ? BusinessRulesMessages.StringCannotBeNullOrEmpty : customError;

        return null;
    }

    public static string? CheckEmailSameWithCurrentUser(string email)
    {
        var currentUserEmail = AuthHelper.GetEmail();

        return currentUserEmail != email ? ServiceResultConstants.EmailIsNotSameWithCurrentUser : null;
    }

    public static string? CheckUsernameSameWithCurrentUser(string username)
    {
        var currentUserUsername = AuthHelper.GetUsername();

        return currentUserUsername != username ? ServiceResultConstants.UsernameIsNotSameWithCurrentUser : null;
    }

    public static string? CheckIdSameWithCurrentUser(Guid id)
    {
        var currentUserId = AuthHelper.GetUserId();

        return currentUserId != id ? ServiceResultConstants.IdIsNotSameWithCurrentUser : null;
    }
}