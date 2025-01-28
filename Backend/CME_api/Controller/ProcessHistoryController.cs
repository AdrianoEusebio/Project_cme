using Microsoft.AspNetCore.Mvc;
[Route("api/processHistory")]
[ApiController]
public class ProcessHistoryController : ControllerBase
{
    private readonly IProcessHistoryService _processHistoryService;

    public ProcessHistoryController(IProcessHistoryService processHistoryService)
    {
        _processHistoryService = processHistoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProcessHistory()
    {
        var processHistory = await _processHistoryService.GetProcessHistory();
        return Ok(processHistory);
    }
}
