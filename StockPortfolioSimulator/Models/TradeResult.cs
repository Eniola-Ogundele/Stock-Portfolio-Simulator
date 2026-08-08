namespace StockPortfolioSimulator.Models;

public enum TradeResult
{
    Success,
    InvalidQuantity,
    InvalidPrice,
    InsufficientCash,
    AssetNotFound,
    InsufficientShares
}
