namespace Balynn.BestStories.Api.Models
{
    public class AppSettingsModel : ICacheSettings, IStoriesApiSettings, IResponseCacheSettings
    {
        public string StoriesApiUrl { get; set; }
        
        public int CacheExpirationPeriodInSeconds { get; set; }

        public int ResponseCacheDurationSeconds { get; set; }
    }
}