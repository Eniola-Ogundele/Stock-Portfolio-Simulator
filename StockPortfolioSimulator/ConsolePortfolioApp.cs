using StockPortfolioSimulator.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace StockPortfolioSimulator;

public static class ConsolePortfolioApp {

    public static void Run()
    {
        Console.WriteLine("Stock Portfolio Simulator");
        Portfolio portfolio = CreatePortfolioFromInput();
        Holding holding = CreateHoldingFromInput();
        portfolio.AddHolding(holding);
        DisplayPortfolio(portfolio);

       }

    private static Portfolio CreatePortfolioFromInput()
    {
        decimal startingCashBalance = ReadDecimal("Enter starting cash balance: $");
        Portfolio portfolio = new Portfolio(startingCashBalance);

        return portfolio;
    }

    private static Holding CreateHoldingFromInput()
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!;

        Console.Write("Enter company name: ");
        string name = Console.ReadLine()!;

        decimal quantity = ReadDecimal("Enter quantity owned: ");

        decimal averagePurchasePrice = ReadDecimal("Enter average purchase price: ");

        Asset asset = new Asset(symbol, name);

        Holding holding = new Holding(asset, quantity, averagePurchasePrice);

        return holding;
    }

    private static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine();
        Console.WriteLine("Portfolio created!");
        Console.WriteLine(portfolio);
        foreach (Holding item in portfolio.Holdings)
        {
            Console.WriteLine($"- {item}");
        }
        Console.WriteLine();
    }

    private static decimal ReadDecimal(string prompt)
    {
        Console.Write(prompt);
        return decimal.Parse(Console.ReadLine()!);
    }
}
