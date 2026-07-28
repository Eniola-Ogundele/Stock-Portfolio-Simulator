namespace Stock-Portfolio-Simulator.Models;
public class Assets
{
	public String Symbol { get; set; }
	public String Name { get; set; }

	public Assets(string symbol, string name)
	{
		Symbol = symbol;
		Name = name;
	}

	public override string ToString()
	{
		return $"{Symbol} - {Name}";
	}
}