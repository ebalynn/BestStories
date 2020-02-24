namespace Balynn.BestStories.Api.Models
{
    public class AppSettingsModel : ICacheSettings, IStoriesApiSettings
    {
        public string StoriesApiUrl { get; set; }
        
        public int CacheExpirationPeriodInSeconds { get; set; }
    }
}