namespace Core.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(Guid userId, string email, string role);
        Guid? ValidateToken(string token);
    }
}
