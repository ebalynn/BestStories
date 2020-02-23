﻿using System.Collections.Generic;
using Balynn.BestStories.Models;

namespace Balynn.BestStories.Tests.UnitTest
{
    internal class ShouldBase
    {
        protected static IEnumerable<StoryModel> GenerateStories(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new StoryModel { Score = i };
            }
        }
    }
}