public interface IUserService
{
    Task<User?> Authenticate(string username, string password);
    string GenerateJwtToken(User user);
}
