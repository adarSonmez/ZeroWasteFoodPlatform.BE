namespace Core.Security.SessionManagement;

public interface ITokenHandler
{
    Token? GenerateToken(Guid userId, string username, string email, string role, bool? infiniteExpiration);
}