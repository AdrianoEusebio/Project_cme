using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class UserService : IUserService
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _config;

    public UserService(MyDbContext context, IConfiguration configuration)
    {
        _config = configuration;
        _context = context;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (BCrypt.Net.BCrypt.Verify(password,user.HashPassword))
            return null;

        if (!user.VerifyPassword(password, user.GenerateHashPassword(password)))
            return null;

        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key  = Encoding.ASCII.GetBytes(s: _config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IdGroup.ToString())

            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}