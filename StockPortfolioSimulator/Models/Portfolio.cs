using System;
using System.Linq;
namespace StockPortfolioSimulator.Models;

public class Portfolio
{
    private readonly List<Holding> _holdings = new();
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyList<Holding> Holdings => _holdings;
    public IReadOnlyList<Transaction> Transactions => _transactions;

    public decimal CashBalance
    {
        get;
        private set;
    }

    public Portfolio(decimal cashBalance)
    {
        CashBalance = cashBalance;
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
            _holdings.Add(holding);
        }

        Transaction transaction = new Transaction(asset, TransactionType.Buy, quantity, purchasePrice);
        _transactions.Add(transaction);
        return true;
    }

    public bool TrySell(string symbol, decimal quantity, decimal salePrice)
    {
        if (quantity <= 0 || salePrice <= 0)
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
            _holdings.Remove(existingHolding);
        }

        Transaction transaction = new Transaction(existingHolding.Asset, TransactionType.Sell, quantity, salePrice);
        _transactions.Add(transaction);
        return true;
    }

    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance:F2}, Holdings: {Holdings.Count}";
    }
}
