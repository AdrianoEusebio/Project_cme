using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/processHistory")]
[ApiController]
public class ProcessHistoryController : ControllerBase
{
    private readonly MyDbContext _context;

    public ProcessHistoryController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProcessHistory()
    {
        var processHistory = await _context.ProcessHistories.ToListAsync();
        return Ok(processHistory);
    }
}
