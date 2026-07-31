using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator;

//Handles all console-based user interactions for the console
public static class ConsolePortfolioApp {

    //Controls the overall flow of the application
    public static void Run()
    {
        Console.WriteLine("Stock Portfolio Simulator");
        Portfolio portfolio = CreatePortfolioFromInput();
        Holding holding = CreateHoldingFromInput();

        //Add the new created holding to the portfolio
        portfolio.AddHolding(holding);

        //Dispaly the completed portfolio
        DisplayPortfolio(portfolio);

       }

    //Prompts the user a starting cash balance and creates a portfolio
    private static Portfolio CreatePortfolioFromInput()
    {
        decimal startingCashBalance = ReadDecimal("Enter starting cash balance: $");
        Portfolio portfolio = new Portfolio(startingCashBalance);

        return portfolio;
    }

    //Prompts the user for stock information and creates a holding
    private static Holding CreateHoldingFromInput()
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!;

        Console.Write("Enter company name: ");
        string name = Console.ReadLine()!;

        decimal quantity = ReadDecimal("Enter quantity owned: ");

        decimal averagePurchasePrice = ReadDecimal("Enter average purchase price: ");

        //Create the asset represented by the stock
        Asset asset = new Asset(symbol, name);

        //Create a holding representing ownership of the asset
        Holding holding = new Holding(asset, quantity, averagePurchasePrice);

        return holding;
    }

    //Displays the portfolio and all assets it contains
    private static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine();
        Console.WriteLine("Portfolio created!");
        Console.WriteLine(portfolio);

        //Displays each holding in the portfolio
        foreach (Holding item in portfolio.Holdings)
        {
            Console.WriteLine($"- {item}");
        }
        Console.WriteLine();
    }

    //Reads a decimal value from the console after displaying a prompt
    private static decimal ReadDecimal(string prompt)
    {
        Console.Write(prompt);
        return decimal.Parse(Console.ReadLine()!);
    }
}
