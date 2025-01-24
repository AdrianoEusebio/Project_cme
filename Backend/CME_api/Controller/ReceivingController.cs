
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReceivingController : ControllerBase
{
    private readonly MyDbContext _context;

    public ReceivingController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Receiving>> PostReceiving(Receiving receiving)
    {
        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == receiving.SerialMaterial &&
                                     (m.Status == MaterialStatus.SEM_PROCESSOS || m.Status == MaterialStatus.DISTRIBUIDO));

        if (material == null)
            return BadRequest("Material não pode ser recebido.");

        material.Status = MaterialStatus.RECEBIDO;
        await _context.SaveChangesAsync();

        _context.Receivings.Add(receiving);
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = receiving.SerialMaterial,
            IdUser = receiving.IdUser,
            EntryData = receiving.EntryData,
            IdReceiving = receiving.IdReceiving,
            EnumStatus = MaterialStatus.RECEBIDO
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReceivings), new { id = receiving.IdReceiving }, receiving);
    }

    [HttpGet]
    public async Task<IActionResult> GetReceivings()
    {
        var receivings = await _context.Materials
            .Where(m => m.Status == MaterialStatus.SEM_PROCESSOS || m.Status == MaterialStatus.DISTRIBUIDO)
            .ToListAsync();
        return Ok(receivings);
    }
}