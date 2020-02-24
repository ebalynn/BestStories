using System.Collections.Generic;
using Balynn.BestStories.Api.Models;

namespace Balynn.BestStories.Api.Services
{
    public interface IStoriesCachingService
    {
        IReadOnlyCollection<StoryModel> Get();
        void Store(IEnumerable<StoryModel> stories);
    }
}