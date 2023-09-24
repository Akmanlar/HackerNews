using HackerNews.Data.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Data;

public class CachedHackerNewsClient : IHackerNewsClient
{
    private const string CacheKey = nameof(CachedHackerNewsClient);
    private readonly IMemoryCache _memoryCache;
    private readonly IHackerNewsClient _hackerNewsClient;

    public CachedHackerNewsClient(IMemoryCache memoryCache, IHackerNewsClient hackerNewsClient)
    {
        _memoryCache = memoryCache;
        _hackerNewsClient = hackerNewsClient;
    }

    public async Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken)
    {
        const string cacheKey = $"{CacheKey}_{nameof(GetBestStoryIdsAsync)}";
        if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<int> result))
            return result;

        result = await _hackerNewsClient.GetBestStoryIdsAsync(cancellationToken);
        _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

        return result;
    }

    public async Task<HackerNewsItemModel> GetItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"{CacheKey}_{nameof(GetItemByIdAsync)}_{id}";
        if (_memoryCache.TryGetValue(cacheKey, out HackerNewsItemModel result))
            return result;

        result = await _hackerNewsClient.GetItemByIdAsync(id, cancellationToken);
        _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

        return result;
    }
}