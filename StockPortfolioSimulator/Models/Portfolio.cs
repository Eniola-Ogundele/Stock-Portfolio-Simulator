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

    public List<Transaction> Transactions
    {
        get;
    }

    public Portfolio(decimal cashBalance)
    {
        CashBalance = cashBalance;
        Holdings = new List<Holding>();
        Transactions = new List<Transaction>();
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

        Transaction transaction = new Transaction(asset, TransactionType.Buy, quantity, purchasePrice);
        Transactions.Add(transaction);
        return true;
    }

    public bool TrySell(string symbol, decimal quantity, decimal salePrice)
    {
        if (quantity <=0 || salePrice <= 0)
        {
            return false;
        }

        Holding? existingHolding = Holdings.FirstOrDefault(h => h.Asset.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

        if (existingHolding == null)
        {
            return false;
        }

        if (quantity > existingHolding.Quantity)
        {
            return false;
        }

        decimal proceeds = quantity * salePrice;
        CashBalance += proceeds;
        existingHolding.RemoveQuantity(quantity);

        if (existingHolding.Quantity == 0)
        {
            Holdings.Remove(existingHolding);
        }

        Transaction transaction = new Transaction(existingHolding.Asset, TransactionType.Sell, quantity, salePrice);
        Transactions.Add(transaction);
        return true;
    }

    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance}, Holdings: {Holdings.Count}";
    }
}