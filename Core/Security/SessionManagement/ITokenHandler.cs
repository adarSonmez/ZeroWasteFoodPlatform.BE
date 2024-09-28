namespace Core.Security.SessionManagement;

/// <summary>
/// Represents an interface for handling tokens.
/// </summary>
public interface ITokenHandler
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="username">The username.</param>
    /// <param name="email">The email.</param>
    /// <param name="role">The role.</param>
    /// <param name="infiniteExpiration">Indicates if the token has infinite expiration.</param>
    /// <returns>The generated token.</returns>
    Token? GenerateToken(Guid userId, string username, string email, string role, bool? infiniteExpiration);
}