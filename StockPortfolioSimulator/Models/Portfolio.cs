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

    public override string ToString()
    {
        return $"Cash Balance: ${CashBalance}, Holdings: {Holdings.Count}";
    }
}