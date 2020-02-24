using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Api.Models;

namespace Balynn.BestStories.Api.EndPoints
{
    public interface IStoriesEndPoint
    {
        Task<IReadOnlyCollection<StoryModel>> GetBestStoriesAsync(CancellationToken ctx);
    }
}