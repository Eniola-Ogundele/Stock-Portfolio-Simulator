namespace StockPortfolioSimulator.Models;

//Represents an investment portfolio that containing cash and holdings
public class Portfolio
{
    //The amount of cash currently available in the portfolio
    public decimal CashBalance
    {
        get; private set;
    }

    //The holdings currently owned by the portfolio
    public List<Holding> Holdings
    {
        get;
    }

    public Portfolio(decimal cashBalance)
    {
        CashBalance = cashBalance;
        Holdings = new List<Holding>();
    }

    /* Attempts to purchase an asset using the available cash balance
     Returns true if the purchase succeeds; otherwise returns false
    */
    public bool TryBuy(Asset asset, decimal quantity, decimal purchasePrice)
    {

        //Calculate the total cost of the purchase
        decimal totalCost = quantity * purchasePrice;

        //The purchase cannot be completed if there is not enough cash.
        if (totalCost > CashBalance)
        {
            return false;
        }

        //Deduct the purchase cost from the cash balance
        CashBalance -= totalCost;

        //Create a holding representing the purchased asset
        Holding holding = new Holding(asset, quantity, purchasePrice);

        //Add the holding to the portfolio
        Holdings.Add(holding);

        return true;
    }

    //Returns a readable summary of the portfolio
    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance}, Holdings: {Holdings.Count}";
    }
}