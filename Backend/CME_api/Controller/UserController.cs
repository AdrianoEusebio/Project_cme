using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context;
    public UserController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return BadRequest(new { message = "Username já está em uso!" });

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.HashPassword);

        var user = new User
        {
            Username = request.Username,
            HashPassword = passwordHash,
            Email = request.Email,
            IdGroup = request.IdGroup,
            CriadoEm = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuário cadastrado com sucesso!" });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound("Usuario Não Encontrado");
        }
        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserRegisterRequestDto request)
    {
        var existingUser = await _context.Users.FindAsync(id);

        if (existingUser == null)
        {
            return NotFound("Usuário não encontrado");
        }

        existingUser.Username = request.Username;
        existingUser.HashPassword = BCrypt.Net.BCrypt.HashPassword(request.HashPassword);
        existingUser.Email = request.Email;
        existingUser.IdGroup = request.IdGroup;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Erro ao atualizar usuário");
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, [FromBody] AdminAuthDto adminAuth)
    {

        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete == null)
            return NotFound(new { message = "Usuário não encontrado" });

        //bool usuarioEmUso = await _context.Materiais.AnyAsync(m => m.IdGroup == id); // Exemplo de verificação
        //if (usuarioEmUso)
        //return BadRequest(new { message = "Usuário já foi utilizado em processos e não pode ser deletado" });

        var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == adminAuth.Username);

        if (adminUser == null || !BCrypt.Net.BCrypt.Verify(adminAuth.Password, adminUser.HashPassword))
            return Unauthorized(new { message = "Credenciais inválidas" });

        if (adminUser.IdGroup != 1) // 1 = Admin
            return Forbid();

        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}