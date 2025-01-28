public interface IUserService
{
    string GenerateHashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateJwtToken(User user);
}