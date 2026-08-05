using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator;

public static class ConsolePortfolioApp {
    public static void Run()
    {
        Console.WriteLine("Stock Portfolio Simulator");
        Portfolio portfolio = CreatePortfolioFromInput();
        while (true)
        {
            DisplayMenu();
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                HandleBuy(portfolio);
            }
            else if (choice == "2")
            {
                HandleSell(portfolio);
            }
            else if (choice == "3")
            {
                DisplayPortfolio(portfolio);
            }

            else if (choice == "4")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option!");
            }
        }
       }
    private static Portfolio CreatePortfolioFromInput()
    {
        decimal startingCashBalance = ReadNonNegativeDecimal("Enter starting cash balance: $");
        Portfolio portfolio = new Portfolio(startingCashBalance);
        return portfolio;
    }
    private static Asset CreateAssetFromInput()
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!;
        Console.Write("Enter company name: ");
        string name = Console.ReadLine()!;
        Asset asset = new Asset(symbol, name);
        return asset;
    }
    private static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine();
        Console.WriteLine(portfolio);
        foreach (Holding item in portfolio.Holdings)
        {
            Console.WriteLine($"- {item}");
        }
        Console.WriteLine();
    }
    private static decimal ReadNonNegativeDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal value) && value >= 0)
            {
                return value;
            }
            Console.WriteLine("Please enter a non-negative number.");
            Console.WriteLine();
        }
    }
    private static decimal ReadPositiveDecimal(string prompt)
    {
        while(true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Please enter a number greater than zero.");
            Console.WriteLine();
        }
    }
    private static void DisplayMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. Buy an asset");
        Console.WriteLine("2. Sell an asset");
        Console.WriteLine("3. View portfolio");
        Console.WriteLine("4. Exit");
        Console.WriteLine();
        Console.Write("Choose an option: ");
    }
    private static void HandleBuy(Portfolio portfolio)
    {
        Asset asset = CreateAssetFromInput();
        decimal quantity = ReadPositiveDecimal("Enter quantity to buy: ");
        decimal purchasePrice = ReadPositiveDecimal("Enter purchase price: ");
        bool purchaseSuccessful = portfolio.TryBuy(asset, quantity, purchasePrice);
        Console.WriteLine();
        if (purchaseSuccessful)
        {
            Console.WriteLine("Purchase successful!");
            DisplayPortfolio(portfolio);
        }
        else
        {
            Console.WriteLine("Purchase failed: insufficient cash.");
        }
    }
    private static void HandleSell(Portfolio portfolio)
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!;
        Holding? holding = portfolio.Holdings.FirstOrDefault(h => h.Asset.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
        if (holding == null)
        {
            Console.WriteLine("Sale failed: Asset not found.");
            return;
        }
        decimal quantity = ReadPositiveDecimal("Enter quantity to sell: ");
        decimal salePrice = ReadPositiveDecimal("Enter sale price: ");
        if (quantity > holding.Quantity)
        {
            Console.WriteLine("Sale failed: insufficient shares.");
            return;
        }
        bool saleSuccessful = portfolio.TrySell(symbol, quantity, salePrice);
        Console.WriteLine();
        if (saleSuccessful)
        {
            Console.WriteLine("Sale Successful!");
            DisplayPortfolio(portfolio);
        }
    }
}
