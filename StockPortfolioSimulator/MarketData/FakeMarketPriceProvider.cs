using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator.MarketData;
public class FakeMarketPriceProvider : IMarketPriceProvider
{
    public decimal GetCurrentPrice(Asset asset)
    {
        return 100m;
    }
}