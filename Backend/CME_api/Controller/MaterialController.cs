using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MaterialController : ControllerBase
{
    private readonly MyDbContext _context;

    public MaterialController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Material>> CreateMaterial(MaterialDto materialDto)
    {
        var material = new Material
        {
            IdUser = materialDto.IdUser,
            ExpirationDate = materialDto.ExpirationDate,
            Type = materialDto.Type,
            Name = materialDto.Name
        };

        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        material.GenerateSerial();
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = material.Serial,
            IdUser = material.IdUser,
            EntryData = DateTime.UtcNow,
            EnumStatus = material.Status,
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMaterialById), new { id = material.IdMaterial }, material);
    }

    // Buscar todos os materiais (GET)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
    {
        return await _context.Materials.ToListAsync();
    }

    // Buscar material por ID (GET)
    [HttpGet("{id}")]
    public async Task<ActionResult<Material>> GetMaterialById(int id)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return material;
    }

    // Deletar material (DELETE)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaterial(int id)
    {
        var material = await _context.Materials.FirstOrDefaultAsync(m => m.IdMaterial == id);

        if (material == null)
        {
            return NotFound("Material Não encontrado");
        }

        var processHistory = await _context.ProcessHistories
        .Where(p => p.SerialMaterial == material.Serial)
        .OrderByDescending(p => p.EntryData)
        .FirstOrDefaultAsync();

        if (processHistory != null)
        {

            processHistory.EnumStatus = MaterialStatus.DESCARTADO;
            _context.ProcessHistories.Update(processHistory);
            await _context.SaveChangesAsync();
        } else
        {
            return NotFound("Histórico de Processo não encontrado");
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}