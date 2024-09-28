using System.Security.Cryptography;
using System.Text;

namespace Core.Utils.Hashing;

/// <summary>
/// Helper class for hashing passwords.
/// </summary>
public static class HashingHelper
{
    /// <summary>
    /// Creates a password hash and salt from the given password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="passwordHash">The generated password hash.</param>
    /// <param name="passwordSalt">The generated password salt.</param>
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    /// <summary>
    /// Verifies if the given password matches the stored password hash and salt.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="passwordHash">The stored password hash.</param>
    /// <param name="passwordSalt">The stored password salt.</param>
    /// <returns>True if the password is verified, otherwise false.</returns>
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}