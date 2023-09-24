using HackerNews.Data;
using HackerNews.Service.Model;

namespace HackerNews.Service;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsClient _hackerNewsClient;

    public HackerNewsService(IHackerNewsClient hackerNewsClient)
    {
        _hackerNewsClient = hackerNewsClient;
    }

    public async Task<IEnumerable<BestStoryServiceResponseModel>> GetBestStoriesAsync(int storyCount,
        CancellationToken cancellationToken)
    {
        if (storyCount < 0)
            return new List<BestStoryServiceResponseModel>();

        var bestStoryIds = (await _hackerNewsClient.GetBestStoryIdsAsync(cancellationToken)).ToArray();

        if (bestStoryIds.Length < 1)
            return new List<BestStoryServiceResponseModel>();

        if (storyCount < bestStoryIds.Length)
            bestStoryIds = bestStoryIds.Take(storyCount).ToArray();

        var tasks = bestStoryIds
            .Select(storyId => _hackerNewsClient.GetItemByIdAsync(storyId, cancellationToken))
            .ToArray();

        await Task.WhenAll(tasks);

        //Since best story ids we get from the service is ordered by score descending
        //and the order is preserved by WhenAll, there's no need to reorder.
        //If a reorder is needed due to cache mismatch between endpoints, OrderByDescending(p=>p.Score) should suffice.
        return tasks.Select(p => p.Result)
            .Select(item => new BestStoryServiceResponseModel
            {
                Title = item.Title,
                Uri = item.Url,
                PostedBy = item.By,
                Time = DateTime.UnixEpoch.AddSeconds(item.Time).ToString("yyyy-MM-ddTHH:mm:sszzz"),
                Score = item.Score,
                CommentCount = item.Descendants
            });
    }
}