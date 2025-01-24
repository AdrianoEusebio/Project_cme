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
    public async Task<ActionResult<Materials>> CreateMaterial(MaterialDTO materialDto)
    {
        var material = new Materials
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

        return CreatedAtAction(nameof(GetMaterialById), new { id = material.Id }, material);
    }

    // Buscar todos os materiais (GET)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Materials>>> GetAllMaterials()
    {
        return await _context.Materials.ToListAsync();
    }

    // Buscar material por ID (GET)
    [HttpGet("{id}")]
    public async Task<ActionResult<Materials>> GetMaterialById(int id)
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
        var material = await _context.Materials.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}