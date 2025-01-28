using Microsoft.EntityFrameworkCore;

public class WashingService : IWashingService
{
    private readonly MyDbContext _context;

    public WashingService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Washing?> StartWashing(WashingDto washingDto)
    {
        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == washingDto.SerialMaterial &&
                                      m.Status == MaterialStatus.RECEBIDO.ToString());

        if (material == null)
            return null;

        material.Status = MaterialStatus.LAVAGEM_INICIADA.ToString();
        await _context.SaveChangesAsync();

        var washing = new Washing
        {
            SerialMaterial = washingDto.SerialMaterial,
            IdUser = washingDto.IdUser
        };

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

        return washing;
    }

    public async Task<bool?> FinishWashing(WashingDto washingDto)
    {
        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == washingDto.SerialMaterial &&
                                      m.Status == MaterialStatus.LAVAGEM_INICIADA.ToString());

        if (material == null)
            return null;

        var washing = await _context.Washings
            .FirstOrDefaultAsync(w => w.SerialMaterial == washingDto.SerialMaterial && w.IsWashed == false);

        if (washing == null)
            return false;

        material.Status = MaterialStatus.LAVAGEM_FINALIZADA.ToString();
        await _context.SaveChangesAsync();

        washing.IsWashed = true;
        washing.IdUser = washingDto.IdUser;
        washing.EntryDate = DateTime.UtcNow;
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

        return true;
    }
    public async Task<IEnumerable<Washing>> GetAllWashings()
    {
        return await _context.Washings.ToListAsync();
    }
    public async Task<IEnumerable<Material>> GetReceivedMaterials()
    {
        return await _context.Materials
            .Where(m => m.Status == MaterialStatus.RECEBIDO.ToString())
            .ToListAsync();
    }
}
