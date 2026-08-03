using System;
using System.Linq;
namespace StockPortfolioSimulator.Models;

public class Portfolio
{
   
    public decimal CashBalance
    {
        get; private set;
    }

    public List<Holding> Holdings
    {
        get;
    }

    public Portfolio(decimal cashBalance)
    {
        CashBalance = cashBalance;
        Holdings = new List<Holding>();
    }

    public bool TryBuy(Asset asset, decimal quantity, decimal purchasePrice)
    {
        if (quantity <= 0 || purchasePrice <= 0)
        {
            return false;
        }

        decimal totalCost = quantity * purchasePrice;

        if (totalCost > CashBalance)
        {
            return false;
        }

        CashBalance -= totalCost;

        Holding? existingHolding = Holdings.FirstOrDefault(h => h.Asset.Symbol.Equals(asset.Symbol, StringComparison.OrdinalIgnoreCase));

        if (existingHolding != null)
        {
            existingHolding.AddPurchase(quantity, purchasePrice);
        }

        else
        {
            Holding holding = new Holding(asset, quantity, purchasePrice);
            Holdings.Add(holding);
        }

        return true;
    }

    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance}, Holdings: {Holdings.Count}";
    }
}