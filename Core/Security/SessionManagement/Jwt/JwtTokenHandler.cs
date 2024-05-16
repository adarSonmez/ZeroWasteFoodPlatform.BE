using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.SessionManagement.Jwt;

public class JwtTokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public JwtTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token? GenerateToken(Guid userId, string username, string email, string role,
        bool? infiniteExpiration = false)
    {
        Token? token = new();
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        token.ExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessExpiration"]));
        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
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

    private IEnumerable<Claim> SetClaims(string userId, string username, string email, string role)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        ];
        return claims.AsEnumerable();
    }

    private string CreateRefreshToken()
    {
        var number = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(number);
        return Convert.ToBase64String(number);
    }

    public static string? GetClaim(string token, string requestedClaim)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Claims.FirstOrDefault(c => c.Type == requestedClaim)?.Value;
    }
}