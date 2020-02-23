using System.Collections.Generic;
using Balynn.BestStories.Models;

namespace Balynn.BestStories.Services
{
    public interface IStoriesCachingService
    {
        IReadOnlyCollection<StoryModel> Get();
        void Store(IEnumerable<StoryModel> stories);
    }
}