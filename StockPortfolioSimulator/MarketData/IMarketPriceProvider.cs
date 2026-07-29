using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public interface IMarketPriceProvider
{
    decimal GetCurrentPrice(Asset asset);
}