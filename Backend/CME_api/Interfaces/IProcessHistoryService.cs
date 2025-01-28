public interface IProcessHistoryService
{
    Task<IEnumerable<ProcessHistory>> GetProcessHistory();
}
