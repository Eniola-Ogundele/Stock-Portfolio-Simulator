using StockPortfolioSimulator.Models;

Console.WriteLine("Stock Portfolio Simulator");
Console.Write("Enter starting cash balance: $");

decimal startingCashBalance  = decimal.Parse(Console.ReadLine()!);
Portfolio portfolio = new Portfolio(startingCashBalance);

Console.Write("Enter stock symbol: ");
string symbol = Console.ReadLine()!;

Console.Write("Enter company name: ");
string name = Console.ReadLine()!;

Console.Write("Enter quantity owned: ");
decimal quantity = decimal.Parse(Console.ReadLine()!);

Console.Write("Enter average purchase price: ");
decimal averagePurchasePrice = decimal.Parse(Console.ReadLine()!);

Asset asset = new Asset(symbol, name);

Holding holding = new Holding(asset, quantity, averagePurchasePrice);

portfolio.AddHolding(holding);

Console.WriteLine();
Console.WriteLine("Portfolio created!");
Console.WriteLine(portfolio);
foreach (Holding item in portfolio.Holdings)
{
    Console.Write(item);
}
Console.WriteLine();