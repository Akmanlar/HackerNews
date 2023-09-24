using HackerNews.Data.Model;

namespace HackerNews.Data;

public class HackerNewsClient : IHackerNewsClient
{
    private readonly HttpClient _httpClient;

    public HackerNewsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken) =>
        await _httpClient.GetFromJsonAsync<IEnumerable<int>>(
            "v0/beststories.json", cancellationToken);

    public async Task<HackerNewsItemModel> GetItemByIdAsync(int id, CancellationToken cancellationToken) =>
        await _httpClient.GetFromJsonAsync<HackerNewsItemModel>(
            $"v0/item/{id}.json", cancellationToken);
}