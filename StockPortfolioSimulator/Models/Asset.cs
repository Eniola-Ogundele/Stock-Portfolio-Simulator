namespace StockPortfolioSimulator.Models;

//Represents a stock
public class Asset
{
	//The asset's ticker symbol
	public string Symbol { get; }

	//The asset's display name
	public string Name { get; }


	public Asset(string symbol, string name)
	{
		Symbol = symbol;
		Name = name;
	}

	//Returns a readable string representation of the asset
	public override string ToString()
	{
		return $"{Symbol} - {Name}";
	}
}