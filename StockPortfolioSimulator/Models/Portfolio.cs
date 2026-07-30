using StockPortfolioSimulator.Models;

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

    public Portfolio(decimal cashbalance)
    {
        CashBalance = cashbalance;
        Holdings = new List<Holding>();
    }

    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance}, Holdings: {Holdings.Count}";
    }
}