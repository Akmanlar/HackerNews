using HackerNews.Data.Model;

namespace HackerNews.Data;

public interface IHackerNewsClient
{
    Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken);
    Task<HackerNewsItemModel> GetItemByIdAsync(int id, CancellationToken cancellationToken);
}