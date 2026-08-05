namespace StockPortfolioSimulator.Models;
public class Asset
{
	public string Symbol { get; }
	public string Name { get; }

	public Asset(string symbol, string name)
	{
		Symbol = symbol;
		Name = name;
	}
	public override string ToString()
	{
		return $"{Symbol} - {Name}";
	}
}