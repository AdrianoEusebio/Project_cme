using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly MyDbContext _context;
    public UserController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}