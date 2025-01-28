public interface IDistributionService
{
    Task<IEnumerable<Distribution>> GetDistributions();
    Task<Distribution?> CreateDistribution(DistributionDto distributionDto);
}
