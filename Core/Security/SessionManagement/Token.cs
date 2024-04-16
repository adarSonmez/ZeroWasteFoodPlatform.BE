namespace Core.Security.SessionManagement;

public class Token
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
    public string? RefreshToken { get; set; }
}