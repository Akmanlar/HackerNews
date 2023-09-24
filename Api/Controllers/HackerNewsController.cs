using HackerNews.Service;
using HackerNews.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Controllers;

[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("BestStories")]
    public async Task<IEnumerable<BestStoryServiceResponseModel>> GetBestStories(int storyCount,
        CancellationToken cancellationToken)
    {
        return await _hackerNewsService.GetBestStoriesAsync(storyCount, cancellationToken);
    }
}