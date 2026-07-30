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

    public override string ToString()
    {
        return $"{Asset} | Quantity: {Quantity}";
    }

}