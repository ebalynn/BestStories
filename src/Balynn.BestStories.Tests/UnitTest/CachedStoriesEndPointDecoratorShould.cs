using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.EndPoints;
using Balynn.BestStories.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Balynn.BestStories.Tests.UnitTest
{

    [TestFixture]
    internal class CachedStoriesEndPointDecoratorShould : ShouldBase
    {
        private Mock<IStoriesEndPoint> _storiesEndPointMock;
        private Mock<IStoriesCachingService> _storiesCachingService;
        private Mock<ILogger<CachedStoriesEndPointDecorator>> _loggerMock;
        private CachedStoriesEndPointDecorator _cachedEndPoint;
        

        [SetUp]
        public void SetUp()
        {
            _storiesEndPointMock = new Mock<IStoriesEndPoint>();
            _storiesCachingService = new Mock<IStoriesCachingService>();
            _loggerMock = new Mock<ILogger<CachedStoriesEndPointDecorator>>();

            _cachedEndPoint = new CachedStoriesEndPointDecorator(_storiesEndPointMock.Object, _storiesCachingService.Object, _loggerMock.Object);
        }

        [Test]
        public async Task UseStoriesCachingService()
        {
            var stories = GenerateStories(200).ToList();

            _storiesEndPointMock.Setup(e => e.GetBestStoriesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> stories);
            
            var result = await _cachedEndPoint.GetBestStoriesAsync(CancellationToken.None);

            Assert.IsNotEmpty(result);

            _storiesCachingService.Setup(c => c.Get()).Returns(() => stories);

            result = await _cachedEndPoint.GetBestStoriesAsync(CancellationToken.None);

            _storiesEndPointMock.Verify(s => s.GetBestStoriesAsync(It.IsAny<CancellationToken>()), Times.Once);
            
            Assert.IsNotEmpty(result);
        }
    }
}
