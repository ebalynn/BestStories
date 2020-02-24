namespace Balynn.BestStories.Api.Models
{
    public interface ICacheSettings
    {
        int CacheExpirationPeriodInSeconds { get; }
    }
}