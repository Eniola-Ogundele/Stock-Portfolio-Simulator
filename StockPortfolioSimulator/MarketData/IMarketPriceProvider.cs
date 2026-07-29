using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator.MarketData;

public interface IMarketPriceProvider
{
    decimal GetCurrentPrice(Asset asset);
}

// Just so this will compile until you have your own Asset Model