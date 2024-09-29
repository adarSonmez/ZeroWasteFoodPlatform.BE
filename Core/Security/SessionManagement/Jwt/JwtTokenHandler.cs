using Core.Utils.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Security.SessionManagement.Jwt;

/// <summary>
/// Represents a handler for generating and validating JWT tokens.
/// </summary>
public class JwtTokenHandler : ITokenHandler
{
    /// <summary>
    /// Gets the value of the requested claim from the JWT token.
    /// </summary>
    /// <param name="token">The JWT token.</param>
    /// <param name="requestedClaim">The requested claim.</param>
    /// <returns>The value of the requested claim.</returns>
    public static string? GetClaim(string token, string requestedClaim)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Claims.FirstOrDefault(c => c.Type == requestedClaim)?.Value;
    }

    /// <summary>
    /// Generates a JWT token with the specified user information.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="username">The username.</param>
    /// <param name="email">The email.</param>
    /// <param name="role">The role.</param>
    /// <param name="infiniteExpiration">Indicates if the token should have an infinite expiration.</param>
    /// <returns>The generated token.</returns>
    public Token? GenerateToken(Guid userId, string username, string email, string role, bool? infiniteExpiration = false)
    {
        Token? token = new();
        var configuration = ServiceTool.GetService<IConfiguration>()!;
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        token.ExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:AccessExpiration"]));
        var jwtSecurityToken = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            expires: infiniteExpiration == true ? DateTime.MaxValue : token.ExpirationTime,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials,
            claims: SetClaims(userId.ToString(), username, email, role)
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;
    }

    /// <summary>
    /// Sets the claims for the JWT token.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="username">The username.</param>
    /// <param name="email">The email.</param>
    /// <param name="role">The role.</param>
    /// <returns>The claims for the JWT token.</returns>
    private IEnumerable<Claim> SetClaims(string userId, string username, string email, string role)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
        return claims.AsEnumerable();
    }

    /// <summary>
    /// Creates a refresh token.
    /// </summary>
    /// <returns>The refresh token.</returns>
    private string CreateRefreshToken()
    {
        var number = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}