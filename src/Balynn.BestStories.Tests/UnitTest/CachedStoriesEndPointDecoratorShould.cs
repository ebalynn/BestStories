using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Api.EndPoints;
using Balynn.BestStories.Api.Services;
using Moq;
using NUnit.Framework;

namespace Balynn.BestStories.Tests.UnitTest
{

    [TestFixture]
    internal class CachedStoriesEndPointDecoratorShould 
    {
        private Mock<IStoriesEndPoint> _storiesEndPointMock;
        private Mock<IStoriesCachingService> _storiesCachingService;
        private CachedStoriesEndPointDecorator _cachedEndPoint;
        

        [SetUp]
        public void SetUp()
        {
            _storiesEndPointMock = new Mock<IStoriesEndPoint>();
            _storiesCachingService = new Mock<IStoriesCachingService>();

            _cachedEndPoint = new CachedStoriesEndPointDecorator(_storiesEndPointMock.Object, _storiesCachingService.Object);
        }

        [Test]
        public async Task UseCachingService()
        {
            var stories = StoriesTestHelper.GenerateStories(200).ToList();

            _storiesEndPointMock.Setup(e => e.GetBestStoriesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> stories);
            
            var result = await _cachedEndPoint.GetBestStoriesAsync(CancellationToken.None);

            // Expecting a call to StoriesEndPoint to get data
            _storiesEndPointMock.Verify(s => s.GetBestStoriesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.AreEqual(200, result.Count);

            _storiesCachingService.Setup(c => c.Get()).Returns(() => stories);

            result = await _cachedEndPoint.GetBestStoriesAsync(CancellationToken.None);

            // Verifying that after 2 calls the underlying service has only been called once and instead the data must have been retrieved from the cache
            _storiesEndPointMock.Verify(s => s.GetBestStoriesAsync(It.IsAny<CancellationToken>()), Times.Once);
            
            Assert.AreEqual(200, result.Count);
        }
    }
}
