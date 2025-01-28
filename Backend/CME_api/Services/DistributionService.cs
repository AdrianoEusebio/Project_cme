using Microsoft.EntityFrameworkCore;

public class DistributionService : IDistributionService
{
    private readonly MyDbContext _context;

    public DistributionService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Distribution>> GetDistributions()
    {
        return await _context.Distributions.ToListAsync();
    }

    public async Task<Distribution?> CreateDistribution(DistributionDto distributionDto)
    {
        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Serial == distributionDto.SerialMaterial
            && m.Status == MaterialStatus.LAVAGEM_FINALIZADA.ToString());

        if (material == null)
            return null;

        material.Status = MaterialStatus.DISTRIBUIDO.ToString();
        await _context.SaveChangesAsync();

        var distribution = new Distribution
        {
            SerialMaterial = distributionDto.SerialMaterial,
            Sector = distributionDto.Sector,
            IdUser = distributionDto.IdUser
        };

        _context.Distributions.Add(distribution);
        await _context.SaveChangesAsync();

        var process = new ProcessHistory
        {
            SerialMaterial = distribution.SerialMaterial,
            IdUser = distribution.IdUser,
            EntryData = distribution.EntryDate,
            IdDistribution = distribution.IdDistribution,
            EnumStatus = MaterialStatus.DISTRIBUIDO.ToString()
        };

        _context.ProcessHistories.Add(process);
        await _context.SaveChangesAsync();

        return distribution;
    }
}
