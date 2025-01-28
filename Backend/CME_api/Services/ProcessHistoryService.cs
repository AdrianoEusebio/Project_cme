using Microsoft.EntityFrameworkCore;

public class ProcessHistoryService : IProcessHistoryService
{
    private readonly MyDbContext _context;

    public ProcessHistoryService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProcessHistory>> GetProcessHistory()
    {
        return await _context.ProcessHistories.ToListAsync();
    }
}
