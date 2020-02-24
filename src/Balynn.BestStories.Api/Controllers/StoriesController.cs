using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Balynn.BestStories.Api.EndPoints;
using Balynn.BestStories.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balynn.BestStories.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = Startup.ResponseCacheProfileName)]
    public class StoriesController : ControllerBase
    {
        private const int NumberOfStories = 20;
        private readonly ICachedStoriesEndPointDecorator _storiesRepository;
        private readonly ILogger<StoriesController> _logger;


        public StoriesController(ICachedStoriesEndPointDecorator storiesRepository, ILogger<StoriesController> logger)
        {
            _storiesRepository = storiesRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets the first 20 best stories from Hacker News API in descending order 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Route("best20")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryModel>>> Best20(CancellationToken ctx)
        {
            try
            {
                var stories = await _storiesRepository.GetBestStoriesAsync(ctx);
                var topStories = stories.OrderByDescending(x => x.Score).Take(NumberOfStories);

                return Ok(topStories);
            }
            catch (Exception exception)
            {
                // Something is seriously broken
                _logger.LogError(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }        
}