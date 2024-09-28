using Core.Domain.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Result;
using Core.Utils.Auth;

namespace Core.Utils.Rules;

/// <summary>
/// Contains utility methods for performing common business rules checks.
/// </summary>
public static class BusinessRules
{
    /// <summary>
    /// Runs the specified business rules checks and throws a ValidationException if any check fails.
    /// </summary>
    /// <param name="checkResults">The results of the business rules checks.</param>
    public static void Run(params (string errCode, string? msg)[] checkResults)
    {
        foreach (var (errCode, msg) in checkResults)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                throw new ValidationException(errCode, msg);
            }
        }
    }

    /// <summary>
    /// Checks if the specified entity object is null and returns an error message if it is.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="obj">The entity object to check.</param>
    /// <returns>The error message if the entity is null; otherwise, null.</returns>
    public static string? CheckEntityNull<TEntity>(TEntity? obj)
        where TEntity : IEntity
    {
        return CheckEntityNull(obj, $"{typeof(TEntity).Name} Not Found!");
    }

    /// <summary>
    /// Checks if the specified DTO object is null and returns an error message if it is.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <param name="obj">The DTO object to check.</param>
    /// <param name="customError">The custom error message to return if the DTO is null.</param>
    /// <returns>The error message if the DTO is null; otherwise, null.</returns>
    public static string? CheckDtoNull<TDto>(TDto? obj, string customError = BusinessRulesMessages.NullObjectPassed)
        where TDto : IDto
    {
        if (obj is not null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(customError))
        {
            customError = BusinessRulesMessages.NullObjectPassed;
        }

        return customError;
    }

    /// <summary>
    /// Checks if the specified email address is valid.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>The error message if the email address is not valid; otherwise, null.</returns>
    public static string? CheckEmail(string email)
    {
        return !email.IsValidEmail() ? BusinessRulesMessages.EmailFormatIsNotValid : null;
    }

    /// <summary>
    /// Checks if the specified string is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="customError">The custom error message to return if the string is null or empty.</param>
    /// <returns>The error message if the string is null or empty; otherwise, null.</returns>
    public static string? CheckStringNullOrEmpty(string? str, string? customError = null)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.IsNullOrEmpty(customError) ? BusinessRulesMessages.StringCannotBeNullOrEmpty : customError;
        }

        return null;
    }

    /// <summary>
    /// Checks if the specified email address is different from the current user's email address.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>The error message if the email address is different from the current user's email address; otherwise, null.</returns>
    public static string? CheckEmailSameWithCurrentUser(string email)
    {
        var currentUserEmail = AuthHelper.GetEmail();

        return currentUserEmail != email ? ServiceResultConstants.EmailIsNotSameWithCurrentUser : null;
    }

    /// <summary>
    /// Checks if the specified username is different from the current user's username.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>The error message if the username is different from the current user's username; otherwise, null.</returns>
    public static string? CheckUsernameSameWithCurrentUser(string username)
    {
        var currentUserUsername = AuthHelper.GetUsername();

        return currentUserUsername != username ? ServiceResultConstants.UsernameIsNotSameWithCurrentUser : null;
    }

    /// <summary>
    /// Checks if the specified ID is different from the current user's ID.
    /// </summary>
    /// <param name="id">The ID to check.</param>
    /// <returns>The error message if the ID is different from the current user's ID; otherwise, null.</returns>
    public static string? CheckIdSameWithCurrentUser(Guid id)
    {
        var currentUserId = AuthHelper.GetUserId();

        return currentUserId != id ? ServiceResultConstants.IdIsNotSameWithCurrentUser : null;
    }

    /// <summary>
    /// Checks if the specified entity object is null and returns an error message if it is.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="obj">The entity object to check.</param>
    /// <param name="customError">The custom error message to return if the entity is null.</param>
    /// <returns>The error message if the entity is null; otherwise, null.</returns>
    private static string? CheckEntityNull<TEntity>(TEntity? obj, string customError)
        where TEntity : IEntity
    {
        if (obj is not null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(customError))
        {
            customError = BusinessRulesMessages.NullObjectPassed;
        }

        return customError;
    }
}