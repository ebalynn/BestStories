using System;
using System.Collections.Generic;
using Balynn.BestStories.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Balynn.BestStories.Services
{
    public class StoriesCachingService : IStoriesCachingService, IDisposable
    {
        private readonly IMemoryCache _cache;
        private const string Key = "bestStories";
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;
        
        public StoriesCachingService(IMemoryCache cache, ICacheSettings cacheSettings)
        {
            _cache = cache;

            var cacheExpiration = TimeSpan.FromSeconds(cacheSettings.CacheExpirationPeriodInSeconds);

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(cacheExpiration);
        }

        /// <summary>
        /// Gets the cached stories if not expired
        /// </summary>
        public IReadOnlyCollection<StoryModel> Get()
        {
            if (_cache.TryGetValue(Key, out var result))
                return (List<StoryModel>) result;

            return null;
        }

        /// <summary>
        /// Stores stories in memory cache
        /// </summary>
        public void Store(IEnumerable<StoryModel> stories)
        {
            _cache.Set(Key, stories, _memoryCacheEntryOptions);
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}
