public interface IMaterialService
{
    Task<MaterialDto> CreateMaterial(MaterialDto materialDto);
    Task<IEnumerable<object>> GetAllMaterials();
    Task<object?> GetMaterialById(int id);
    Task<bool?> DeleteMaterial(int id);
}