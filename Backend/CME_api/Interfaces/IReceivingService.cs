public interface IReceivingService
{
    Task<Receiving?> RegisterReceiving(ReceivingDto receivingDto);
    Task<IEnumerable<Material>> GetReceivings();
}
