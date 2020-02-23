using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Models;

namespace Balynn.BestStories.EndPoints
{
    public interface IStoriesEndPoint
    {
        Task<IReadOnlyCollection<StoryModel>> GetBestStoriesAsync(CancellationToken ctx);
    }
}