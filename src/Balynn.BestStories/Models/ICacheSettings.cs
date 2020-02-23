namespace Balynn.BestStories.Models
{
    public interface ICacheSettings
    {
        int CacheExpirationPeriodInSeconds { get; }
    }
}