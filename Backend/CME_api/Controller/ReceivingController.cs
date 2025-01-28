using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReceivingController : ControllerBase
{
    private readonly IReceivingService _receivingService;

    public ReceivingController(IReceivingService receivingService)
    {
        _receivingService = receivingService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Receiving>> PostReceiving([FromBody] ReceivingDto receivingDto)
    {
        var receiving = await _receivingService.RegisterReceiving(receivingDto);

        if (receiving == null)
            return BadRequest("Material não pode ser recebido.");

        return CreatedAtAction(nameof(GetReceivings), new { id = receiving.IdReceiving }, receiving);
    }

    [HttpGet]
    public async Task<IActionResult> GetReceivings()
    {
        var receivings = await _receivingService.GetReceivings();
        return Ok(receivings);
    }
}
