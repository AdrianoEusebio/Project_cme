using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IUserService _userService;

    public UserController(MyDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null)
            return NotFound(new { message = "Usuário não encontrado" });

        if (!_userService.VerifyPassword(request.Password, user.HashPassword))
            return BadRequest(new { message = "Senha incorreta" });

        var token = _userService.GenerateJwtToken(user);
        return Ok(new { IdUser = user.IdUser, Username = user.Username, IdGroup = user.IdGroup, Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return BadRequest(new { message = "Username já está em uso!" });

        var user = new User
        {
            Username = request.Username,
            HashPassword = _userService.GenerateHashPassword(request.HashPassword),
            Email = request.Email,
            IdGroup = request.IdGroup,
            CriadoEm = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuário cadastrado com sucesso!" });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.UserGroup)
            .Select(u => new
            {
                u.IdUser,
                u.Username,
                u.Email,
                u.CriadoEm,
                u.IdGroup,
                UserGroup = u.UserGroup.Name
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserGroup)
            .Where(u => u.IdUser == id)
            .Select(u => new
            {
                u.IdUser,
                u.Username,
                u.Email,
                u.CriadoEm,
                u.IdGroup,
                UserGroup = u.UserGroup.Name
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound(new { message = "Usuário não encontrado" });

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        if (await _context.ProcessHistories.AnyAsync(p => p.IdUser == id))
            return BadRequest(new { message = "Não é possível excluir usuários vinculados a processos." });

        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
