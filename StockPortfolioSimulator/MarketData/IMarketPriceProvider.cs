namespace StockPortfolioSimulator.MarketData;

public interface IMarketPriceProvider
{
    decimal GetCurrentPrice(Asset asset);
}

// Just so this will compile until you have your own Asset Model
// TODO: Delete this class
public sealed class Asset
{
    string Symbol = string.Empty;
    string Name = string.Empty;
}