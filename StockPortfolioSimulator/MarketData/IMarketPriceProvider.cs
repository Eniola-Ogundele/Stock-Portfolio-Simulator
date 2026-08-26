using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public interface IMarketPriceProvider
{
    Task<decimal> GetCurrentPrice(Asset asset);
}