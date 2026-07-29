using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Tests.MarketData;

public class FakeMarketPriceProviderTests
{
    [Fact]
    public void GetCurrentPrice_ReturnsOneHundred()
    {
        // Arrange
        var provider = new FakeMarketPriceProvider();
        var asset = new Asset("MSFT", "Microsoft");

        // Act
        decimal price = provider.GetCurrentPrice(asset);

        // Assert
        // FakeMarketPriceProvider always returns 100 for all assets
        Assert.Equal(100m, price);
    }
}