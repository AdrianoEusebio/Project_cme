using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/distribution")]
[ApiController]
public class DistributionController : ControllerBase
{
    private readonly MyDbContext _context;

    public DistributionController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDistributions()
    {
        var materials = await _context.Materials
            .Where(m => m.Status == MaterialStatus.LAVAGEM_FINALIZADA)
            .ToListAsync();

        return Ok(materials);
    }


    [HttpPost]
    public async Task<IActionResult> CreateDistribution([FromBody] DistributionDto distributionDto)
    {
        var distribution = new Distribution
        {
            SerialMaterial = distributionDto.SerialMaterial,
            Sector = distributionDto.Sector,
            IdUser = distributionDto.IdUser
        };

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == distribution.SerialMaterial
            && m.Status == MaterialStatus.LAVAGEM_FINALIZADA);

        if (material == null)
            return BadRequest("Distribuição só pode ser feita para materiais com status LAVAGEM_FINALIZADA.");

        material.Status = MaterialStatus.DISTRIBUIDO;
        await _context.SaveChangesAsync();


        _context.Distributions.Add(distribution);
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = distribution.SerialMaterial,
            IdUser = distribution.IdUser,
            EntryData = distribution.EntryDate,
            IdDistribution = distribution.IdDistribution,
            EnumStatus = MaterialStatus.DISTRIBUIDO
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDistributions), new { id = distribution.IdDistribution }, distribution);
    }
}
