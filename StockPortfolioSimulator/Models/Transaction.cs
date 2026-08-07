namespace StockPortfolioSimulator.Models;

public class Transaction
{
    public Asset Asset
    {
        get;
    }

    public TransactionType Type
    {
        get;
    }

    public decimal Quantity
    {
        get;
    }

    public decimal Price
    {
        get;
    }

    public DateTime Timestamp
    {
        get;
    }

    public decimal Total => Quantity * Price;

    public Transaction(Asset asset, TransactionType type, decimal quantity, decimal price)
    {
        Asset = asset;
        Type = type;
        Quantity = quantity;
        Price = price;
        Timestamp = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Timestamp} | {Type} | {Asset.Symbol} | Quantity: {Quantity} | Price: ${Price:F2} | Total: ${Total:F2}";
    }
}
