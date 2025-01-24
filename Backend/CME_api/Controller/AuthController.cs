using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly MyDbContext _context;

    public AuthController(IUserService userService, MyDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequiestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
            return NotFound(new { message = "Usuário não encontrado" });

        if (!BCrypt.Net.BCrypt.Verify(request.HashPassword, user.HashPassword))
            return BadRequest(new { message = "Senha incorreta" });

        var token = _userService.GenerateJwtToken(user);
        return Ok(new { Username = user.Username, IdGroup = user.IdGroup, Token = token });
    }
}