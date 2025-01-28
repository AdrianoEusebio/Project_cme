public interface IWashingService
{
    Task<Washing?> StartWashing(WashingDto washingDto);
    Task<bool?> FinishWashing(WashingDto washingDto);
    Task<IEnumerable<Washing>> GetAllWashings();
    Task<IEnumerable<Material>> GetReceivedMaterials();
}
