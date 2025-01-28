using Microsoft.AspNetCore.Mvc;

[Route("api/distribution")]
[ApiController]
public class DistributionController : ControllerBase
{
    private readonly IDistributionService _distributionService;

    public DistributionController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDistributions()
    {
        var distributions = await _distributionService.GetDistributions();
        return Ok(distributions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDistribution([FromBody] DistributionDto distributionDto)
    {
        var distribution = await _distributionService.CreateDistribution(distributionDto);

        if (distribution == null)
            return BadRequest("Distribuição só pode ser feita para materiais com status LAVAGEM_FINALIZADA.");

        return CreatedAtAction(nameof(GetDistributions), new { id = distribution.IdDistribution }, distribution);
    }
}
