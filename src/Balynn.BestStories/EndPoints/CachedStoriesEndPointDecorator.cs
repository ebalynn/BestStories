using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Models;
using Balynn.BestStories.Services;

namespace Balynn.BestStories.EndPoints
{
    /// <summary>
    /// 'Decorator' that adds caching logic 
    /// </summary>
    public class CachedStoriesEndPointDecorator : ICachedStoriesEndPointDecorator, IDisposable
    {
        private readonly IStoriesEndPoint _storiesEndPoint;
        private readonly IStoriesCachingService _cachingService;
        private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);

        public CachedStoriesEndPointDecorator(IStoriesEndPoint storiesEndPoint, IStoriesCachingService cachingService)
        {
            _storiesEndPoint = storiesEndPoint;
            _cachingService = cachingService;
        }
        
        /// <summary>
        /// 'Decorates' the method with caching functionality to improve performance. The implementation is quite naive
        /// in a sense it only caches the data for an absolute time.
        /// Locking within async/await is a bit tricky (for obvious reasons). Ideally I would have wanted to use
        /// ReaderWriterLockerSlim by entering into a reading lock first and then upgrading to a write lock if the
        /// data is not cached.  
        /// </summary>
        public async Task<IReadOnlyCollection<StoryModel>> GetBestStoriesAsync(CancellationToken ctx)
        {
            try
            {
                await _cacheLock.WaitAsync(ctx);

                var cachedStories = _cachingService.Get();

                if (cachedStories == null)
                {
                    var stories = await _storiesEndPoint.GetBestStoriesAsync(ctx);

                    _cachingService.Store(stories);

                    cachedStories = stories;
                }

                return cachedStories;
            }
            finally
            {
                _cacheLock.Release();
            }
        }
        
        public void Dispose()
        {
            _cacheLock?.Dispose();
        }
    }
}
