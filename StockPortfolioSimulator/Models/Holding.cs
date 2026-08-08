namespace StockPortfolioSimulator.Models;

public class Holding
{
    public Asset Asset
    {
        get;
    }

    public decimal Quantity
    {
        get;
        private set;
    }

    public decimal AveragePurchasePrice
    {
        get;
        private set;
    }

    public Holding(Asset asset, decimal quantity, decimal averagePurchasePrice)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        if (averagePurchasePrice <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(averagePurchasePrice));
        }

        Asset = asset;
        Quantity = quantity;
        AveragePurchasePrice = averagePurchasePrice;
    }

    public void AddPurchase(decimal quantity, decimal purchasePrice)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        if (purchasePrice <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(purchasePrice));
        }

        decimal existingCost = Quantity * AveragePurchasePrice;
        decimal newCost = quantity * purchasePrice;
        decimal totalQuantity = quantity + Quantity;
        AveragePurchasePrice = (newCost + existingCost) / totalQuantity;
        Quantity = totalQuantity;
    }

    public void RemoveQuantity(decimal quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        if (quantity > Quantity)
        {
            throw new InvalidOperationException("Not enough shares.");
        }

        Quantity -= quantity;
    }

    public override string ToString()
    {
        return $"{Asset} | Quantity: {Quantity} | Average purchase price: ${AveragePurchasePrice:F2}";
    }
}
