using HackerNews.Data;
using HackerNews.Data.Model;
using HackerNews.Service;
using Moq;
using Xunit;

namespace HackerNews;

public class HackerNewsServiceTest
{
    private readonly Mock<IHackerNewsService> _mockHackerNewsService = new();
    private readonly Mock<IHackerNewsClient> _mockHackerNewsClient = new();

    [Fact]
    public async void GetBestStories_RequestedStoryCountLessThanZero_ReturnsEmpty()
    {
        //Arrange
        var hackerNewsService = new HackerNewsService(_mockHackerNewsClient.Object);

        //Act
        var result = await hackerNewsService.GetBestStoriesAsync(-1, It.IsAny<CancellationToken>());

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async void GetBestStories_NoBestStoryFound_ReturnsEmpty()
    {
        //Arrange
        var hackerNewsService = new HackerNewsService(_mockHackerNewsClient.Object);
        _mockHackerNewsClient.Setup(p => p.GetBestStoryIdsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<int>());

        //Act
        var result = await hackerNewsService.GetBestStoriesAsync(5, It.IsAny<CancellationToken>());

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async void GetBestStories_RequestedStoryCountLessThanStoryCount_ReturnsFilteredResponse()
    {
        //Arrange
        var bestStoryIds = new[] { 1, 2, 3 };

        var bestStory1 = new HackerNewsItemModel
        {
            Id = 1,
            Title = "title",
            Url = "Url",
            By = "By",
            Time = 0,
            Score = 0,
            Descendants = 0
        };

        var bestStory2 = new HackerNewsItemModel
        {
            Id = 2,
            Title = "title",
            Url = "Url",
            By = "By",
            Time = 0,
            Score = 0,
            Descendants = 0
        };

        var bestStory3 = new HackerNewsItemModel
        {
            Id = 3,
            Title = "title",
            Url = "Url",
            By = "By",
            Time = 0,
            Score = 0,
            Descendants = 0
        };

        var service = new HackerNewsService(_mockHackerNewsClient.Object);

        _mockHackerNewsClient.Setup(p => p.GetBestStoryIdsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bestStoryIds);
        _mockHackerNewsClient.Setup(p => p.GetItemByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(bestStory1);
        _mockHackerNewsClient.Setup(p => p.GetItemByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(bestStory2);
        _mockHackerNewsClient.Setup(p => p.GetItemByIdAsync(3, It.IsAny<CancellationToken>())).ReturnsAsync(bestStory3);

        //Act
        var result = await service.GetBestStoriesAsync(1, It.IsAny<CancellationToken>());

        //Assert
        Assert.Equal(1, result.Count());
    }
}