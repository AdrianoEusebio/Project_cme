using Microsoft.EntityFrameworkCore;

public class ReceivingService : IReceivingService
{
    private readonly MyDbContext _context;

    public ReceivingService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Receiving?> RegisterReceiving(ReceivingDto receivingDto)
    {
        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == receivingDto.SerialMaterial &&
                                     (m.Status == MaterialStatus.SEM_PROCESSOS.ToString() ||
                                      m.Status == MaterialStatus.DISTRIBUIDO.ToString()));

        if (material == null)
            return null;

        material.Status = MaterialStatus.RECEBIDO.ToString();
        await _context.SaveChangesAsync();

        var receiving = new Receiving
        {
            SerialMaterial = receivingDto.SerialMaterial,
            IdUser = receivingDto.IdUser,
        };

        _context.Receivings.Add(receiving);
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = receiving.SerialMaterial,
            IdUser = receiving.IdUser,
            EntryData = receiving.EntryData,
            IdReceiving = receiving.IdReceiving,
            EnumStatus = MaterialStatus.RECEBIDO.ToString()
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return receiving;
    }

    public async Task<IEnumerable<Material>> GetReceivings()
    {
        return await _context.Materials
            .Where(m => m.Status == MaterialStatus.SEM_PROCESSOS.ToString() ||
                        m.Status == MaterialStatus.DISTRIBUIDO.ToString())
            .ToListAsync();
    }
}
