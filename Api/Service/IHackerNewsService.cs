using HackerNews.Service.Model;

namespace HackerNews.Service;

public interface IHackerNewsService
{
    Task<IEnumerable<BestStoryServiceResponseModel>>
        GetBestStoriesAsync(int storyCount, CancellationToken cancellationToken);
}