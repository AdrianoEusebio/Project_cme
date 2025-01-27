using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class WashingController : ControllerBase
{
    private readonly MyDbContext _context;

    public WashingController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost("start")]
    public async Task<ActionResult<Washing>> StartWashing([FromBody] WashingDto washingDto)
    {
        var washing = new Washing{
            SerialMaterial = washingDto.SerialMaterial,
            IdUser = washingDto.IdUser
        };

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == washing.SerialMaterial && m.Status == MaterialStatus.RECEBIDO.ToString());

        if (material == null)
            return BadRequest("Lavagem só pode ser iniciada se o material estiver com status RECEBIDO.");

        material.Status = MaterialStatus.LAVAGEM_INICIADA.ToString();
        await _context.SaveChangesAsync();

        _context.Washings.Add(washing);
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = washing.SerialMaterial,
            IdUser = washing.IdUser,
            EntryData = washing.EntryDate,
            IdWashing = washing.IdWashing,
            EnumStatus = MaterialStatus.LAVAGEM_INICIADA.ToString()
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWashings), new { id = washing.IdWashing }, washing);
    }

    [HttpPost("finish")]
    public async Task<IActionResult> FinishWashing([FromBody] WashingDto washingDto)
    {

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == washingDto.SerialMaterial && m.Status == MaterialStatus.LAVAGEM_INICIADA.ToString());

        if (material == null)
            return BadRequest("Lavagem não pode ser finalizada.");

        var washing = await _context.Washings
            .FirstOrDefaultAsync(w => w.SerialMaterial == washingDto.SerialMaterial && w.IsWashed == false);

        if (washing == null)
            return BadRequest("Erro: Não há registro de lavagem pendente para esse material.");

        
        material.Status = MaterialStatus.LAVAGEM_FINALIZADA.ToString();
        await _context.SaveChangesAsync();

        washing.IsWashed = true;
        await _context.SaveChangesAsync();

        washing.IdUser = washingDto.IdUser;
        await _context.SaveChangesAsync();

        washing.EntryDate = DateTime.UtcNow;;
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = washing.SerialMaterial,
            IdUser = washing.IdUser,
            EntryData = washing.EntryDate,
            IdWashing = washing.IdWashing,
            EnumStatus = MaterialStatus.LAVAGEM_FINALIZADA.ToString()
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Lavagem finalizada com sucesso!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetWashings()
    {
        var washings = await _context.Materials
            .Where(m => m.Status == MaterialStatus.RECEBIDO.ToString())
            .ToListAsync();

        return Ok(washings);
    }
}