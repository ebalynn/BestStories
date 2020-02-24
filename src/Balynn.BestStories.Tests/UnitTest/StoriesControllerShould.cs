using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Controllers;
using Balynn.BestStories.EndPoints;
using Balynn.BestStories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Balynn.BestStories.Tests.UnitTest
{
    [TestFixture]
    internal class StoriesControllerShould : ShouldBase
    {
        private const int ResultCount = 20;

        private StoriesController _storiesController;
        private Mock<ICachedStoriesEndPointDecorator> _endPointMock;
        private Mock<ILogger<StoriesController>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _endPointMock = new Mock<ICachedStoriesEndPointDecorator>();
            _endPointMock.Setup(e => e.GetBestStoriesAsync(CancellationToken.None))
                .ReturnsAsync(() => GenerateStories(200).ToList());

            _loggerMock = new Mock<ILogger<StoriesController>>();
            _storiesController = new StoriesController(_endPointMock.Object, _loggerMock.Object);
        }


        [Test]
        public async Task Fetch20Stories()
        {
            var result = await _storiesController.Best20(CancellationToken.None);
           
            Assert.IsTrue(result.Result is OkObjectResult);

            var okResult = result.Result as OkObjectResult;
            var stories = (IEnumerable<StoryModel>) okResult.Value;

            Assert.IsTrue(stories.Count() == ResultCount);
        }


        [Test]
        public async Task ReturnStoriesInDescOrderByScore()
        {
            var result = await _storiesController.Best20(CancellationToken.None);
            var okResult = result.Result as OkObjectResult;
            var stories = (IEnumerable<StoryModel>)okResult.Value;

            for (var i = 0; i < ResultCount - 1; i++)
            {
                Assert.IsTrue(stories.ElementAt(i).Score > stories.ElementAt(i + 1).Score);
            }
        }
    }
}
