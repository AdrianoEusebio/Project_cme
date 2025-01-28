using Microsoft.EntityFrameworkCore;

public class MaterialService : IMaterialService
{
    private readonly MyDbContext _context;

    public MaterialService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<MaterialDto> CreateMaterial(MaterialDto materialDto)
    {
        var material = new Material
        {
            IdUser = materialDto.IdUser,
            ExpirationDate = materialDto.ExpirationDate.ToUniversalTime(),
            Type = materialDto.Type,
            Name = materialDto.Name,
        };

        _context.Materials.Add(material);
        await _context.SaveChangesAsync();

        material.GenerateSerial();
        await _context.SaveChangesAsync();

        return new MaterialDto
        {
            IdUser = material.IdUser,
            ExpirationDate = material.ExpirationDate,
            Type = material.Type,
            Name = material.Name
        };
    }

    public async Task<IEnumerable<object>> GetAllMaterials()
    {
        return await _context.Materials
            .Join(_context.Users,
                material => material.IdUser,
                user => user.IdUser,
                (material, user) => new
                {
                    IdMaterial = material.IdMaterial,
                    Name = material.Name,
                    Serial = material.Serial,
                    Status = material.Status,
                    ExpirationDate = material.ExpirationDate,
                    Type = material.Type,
                    EntryDate = material.EntryDate,
                    IdUser = material.IdUser,
                    UserName = user.Username 
                })
            .ToListAsync();
    }

    public async Task<object?> GetMaterialById(int id)
    {
        return await _context.Materials
            .Join(_context.Users,
                material => material.IdUser,
                user => user.IdUser,
                (material, user) => new
                {
                    IdMaterial = material.IdMaterial,
                    Name = material.Name,
                    Serial = material.Serial,
                    Status = material.Status,
                    ExpirationDate = material.ExpirationDate, 
                    EntryDate = material.EntryDate,
                    IdUser = material.IdUser,
                    UserName = user.Username 
                })
            .FirstOrDefaultAsync(m => m.IdMaterial == id);
    }

    public async Task<bool?> DeleteMaterial(int id)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material == null)
            return null;

        if (material.Status != MaterialStatus.SEM_PROCESSOS.ToString())
            return false;

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync();
        return true;
    }
}
