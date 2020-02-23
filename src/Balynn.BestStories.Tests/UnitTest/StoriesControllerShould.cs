using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Controllers;
using Balynn.BestStories.EndPoints;
using Balynn.BestStories.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Balynn.BestStories.Tests.UnitTest
{
    [TestFixture]
    internal class StoriesControllerShould : ShouldBase
    {
        private StoriesController _storiesController;
        private Mock<ICachedStoriesEndPointDecorator> _endPointMock;
        private Mock<ILogger<StoriesController>> _loggerMock;
        private Mock<IHostingEnvironment> _hostingEnvironment;

        [SetUp]
        public void SetUp()
        {
            _endPointMock = new Mock<ICachedStoriesEndPointDecorator>();
            _hostingEnvironment = new Mock<IHostingEnvironment>();
            _endPointMock.Setup(e => e.GetBestStoriesAsync(CancellationToken.None))
                .ReturnsAsync(() => GenerateStories(200).ToList());

            _loggerMock = new Mock<ILogger<StoriesController>>();
            _storiesController = new StoriesController(_endPointMock.Object, _hostingEnvironment.Object, _loggerMock.Object);
        }


        [Test]
        public async Task FetchTop20Stories()
        {
            var result = await _storiesController.Best20(CancellationToken.None);
           
            Assert.IsTrue(result.Result is OkObjectResult);

            var okResult = result.Result as OkObjectResult;

            Assert.IsTrue(((IEnumerable<StoryModel>) okResult.Value).Count() == 20);
            
            _endPointMock.Verify(endpoint => endpoint.GetBestStoriesAsync(CancellationToken.None), Times.Once);
        }
    }
}
