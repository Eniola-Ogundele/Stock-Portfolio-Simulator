namespace StockPortfolioSimulator.Models;
public class Asset
{
	public String Symbol { get; set; }
	public String Name { get; set; }

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