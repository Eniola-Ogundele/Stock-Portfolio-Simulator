namespace StockPortfolioSimulator.Models;
public class Holding
{
    public Asset Asset { get; }
    public decimal Quantity { get; private set; }
    public decimal AveragePurchasePrice { get; private set; }
    public Holding(Asset asset, decimal quantity, decimal averagePurchasePrice)
    {
        Asset = asset;
        Quantity = quantity;
        AveragePurchasePrice = averagePurchasePrice;
    }
    public void AddPurchase(decimal quantity, decimal purchasePrice)
    {
        decimal existingCost = Quantity * AveragePurchasePrice;
        decimal newCost = quantity * purchasePrice;
        decimal totalQuantity = quantity + Quantity;
        AveragePurchasePrice = (newCost + existingCost) / totalQuantity;
        Quantity = totalQuantity;
    }
    public void RemoveQuantity(decimal quantity)
    {
        Quantity -= quantity;
    }
    public override string ToString()
    {
        return $"{Asset} | Quantity: {Quantity} | Average purchase price: ${AveragePurchasePrice:F2}";
    }

}