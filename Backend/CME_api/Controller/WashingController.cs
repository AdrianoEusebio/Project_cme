using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WashingController : ControllerBase
{
    private readonly IWashingService _washingService;

    public WashingController(IWashingService washingService)
    {
        _washingService = washingService;
    }

    [HttpPost("start")]
    public async Task<ActionResult<Washing>> StartWashing([FromBody] WashingDto washingDto)
    {
        var washing = await _washingService.StartWashing(washingDto);

        if (washing == null)
            return BadRequest("Lavagem só pode ser iniciada se o material estiver com status RECEBIDO.");

        return CreatedAtAction(nameof(GetAllWashings), new { id = washing.IdWashing }, washing);
    }

    [HttpPost("finish")]
    public async Task<IActionResult> FinishWashing([FromBody] WashingDto washingDto)
    {
        var result = await _washingService.FinishWashing(washingDto);

        if (result == null)
            return BadRequest("Lavagem não pode ser finalizada.");

        if (!result.Value)
            return BadRequest("Erro: Não há registro de lavagem pendente para esse material.");

        return Ok(new { message = "Lavagem finalizada com sucesso!" });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllWashings()
    {
        var washings = await _washingService.GetAllWashings();
        return Ok(washings);
    }

    [HttpGet("received")]
    public async Task<IActionResult> GetReceivedMaterials()
    {
        var receivedMaterials = await _washingService.GetReceivedMaterials();
        return Ok(receivedMaterials);
    }
}
