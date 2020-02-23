using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Models;
using Microsoft.Extensions.Logging;

namespace Balynn.BestStories.EndPoints
{
    public class StoriesEndPoint : IStoriesEndPoint
    {
        private readonly ILogger<StoriesEndPoint> _logger;
        private const string BestStoriesUri = "beststories.json";
        private readonly string _storiesApiUrl;


        public StoriesEndPoint(ILogger<StoriesEndPoint> logger, IStoriesApiSettings storiesApiSettings)
        {
            _logger = logger;
            _storiesApiUrl = storiesApiSettings.StoriesApiUrl;
        }

        /// <summary>
        /// Gets all best stories from Hacker News API
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<StoryModel>> GetBestStoriesAsync(CancellationToken ctx)
        {
            var result = new List<StoryModel>();
            
            _logger.LogInformation($"Getting best stories from {_storiesApiUrl}...");
            
            var sw = Stopwatch.StartNew();
            
            try
            {
                ctx.ThrowIfCancellationRequested();

                var storyIds = await GetBestStoryIds(ctx);
                
                using var client = CreateHttpClient();

                foreach (var storyId in storyIds)
                {
                    ctx.ThrowIfCancellationRequested();

                    var story = await GetStoryAsync(ctx, storyId, client)
                        .ConfigureAwait(false);

                    result.Add(story);
                }
            }
            finally
            {
                sw.Stop();

                _logger.LogInformation($"Time taken to retrieve '{result.Count}' stories: {sw.Elapsed}");
            }

            return result;
        }

        protected async Task<int[]> GetBestStoryIds(CancellationToken ctx)
        {
            ctx.ThrowIfCancellationRequested();

            using var client = CreateHttpClient();

            var storyIdResponse = await client.GetAsync(BestStoriesUri, ctx);

            storyIdResponse.EnsureSuccessStatusCode();

            return await storyIdResponse.Content.ReadAsAsync<int[]>(ctx);
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(_storiesApiUrl)
            };
        }

        private static async Task<StoryModel> GetStoryAsync(CancellationToken ctx, int storyId, HttpClient client)
        {
            var storyUri = $"item/{storyId}.json";
            var response = await client.GetAsync(storyUri, ctx)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var story = await response.Content.ReadAsAsync<StoryModel>(ctx)
                .ConfigureAwait(false);
            return story;
        }
    }
}
