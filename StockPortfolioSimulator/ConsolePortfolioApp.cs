using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator;

//Handles all console-based user interactions for the console
public static class ConsolePortfolioApp {

    //Controls the overall flow of the application
    public static void Run()
    {
        Console.WriteLine("Stock Portfolio Simulator");

        //Create a portfolio using the user's starting balance
        Portfolio portfolio = CreatePortfolioFromInput();

        //Create the asset the user would like to purchase
        Asset asset = CreateAssetFromInput();

        //Read the purchase information from the user
        decimal quantity = ReadDecimal("Enter quantity to buy: ");
        decimal purchasePrice = ReadDecimal("Enter purchase price: ");

        //Attempt to buy the asset using the Portfolio business logic
        bool purchaseSuccessful = portfolio.TryBuy(asset, quantity, purchasePrice);
        Console.WriteLine();
        if (purchaseSuccessful)
        {
            Console.WriteLine("Purchase successful.");

            //Display the updated portfolio after the purchase
            DisplayPortfolio(portfolio);

        }
        else
        {
            Console.WriteLine("Purchase failed: insufficient cash.");
        }

       }

    //Prompts the user a starting cash balance and creates a portfolio
    private static Portfolio CreatePortfolioFromInput()
    {
        decimal startingCashBalance = ReadDecimal("Enter starting cash balance: $");
        Portfolio portfolio = new Portfolio(startingCashBalance);

        return portfolio;
    }

    //Prompts the user for stock information and creates an asset
    private static Asset CreateAssetFromInput()
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!;

        Console.Write("Enter company name: ");
        string name = Console.ReadLine()!;

        Asset asset = new Asset(symbol, name);
        return asset;
    }

    //Displays the portfolio and all holdings it contains
    private static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine();
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
