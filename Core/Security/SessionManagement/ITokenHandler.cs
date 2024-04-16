namespace Core.Security.SessionManagement;

public interface ITokenHandler
{
    Token? GenerateToken(string userId, string username, string email, string role, bool? infiniteExpiration);
}