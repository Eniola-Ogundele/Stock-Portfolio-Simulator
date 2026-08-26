using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public class FakeMarketPriceProvider : IMarketPriceProvider
{
    public Task<decimal> GetCurrentPrice(Asset asset)
    {
        return Task.FromResult(100m);
    }
}