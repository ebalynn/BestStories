using System.Collections.Generic;
using Balynn.BestStories.Api.Models;

namespace Balynn.BestStories.Tests.UnitTest
{
    internal static class StoriesTestHelper
    {
        public static IEnumerable<StoryModel> GenerateStories(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new StoryModel { Score = i };
            }
        }
    }
}