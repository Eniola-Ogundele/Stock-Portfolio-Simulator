namespace StockPortfolioSimulator.Models;

//Represents ownership of a quantity of an asset within a portfolio
public class Holding
{

    //The asset that is owned
    public Asset Asset { get; }

    //The number of units owned
    public decimal Quantity { get; private set; }

    //The average price paid per unit
    public decimal AveragePurchasePrice { get; private set; }

    public Holding(Asset asset, decimal quantity, decimal averagePurchasePrice)
    {
        Asset = asset;
        Quantity = quantity;
        AveragePurchasePrice = averagePurchasePrice;

    }

    //Returns a readable string representation of the holding
    public override string ToString()
    {
        return $"{Asset} | Quantity: {Quantity}";
    }

}