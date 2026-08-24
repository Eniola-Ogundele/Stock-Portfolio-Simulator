using StockPortfolioSimulator.Models;
using StockPortfolioSimulator.Persistence;
namespace StockPortfolioSimulator;

public static class ConsolePortfolioApp
{
    public static async Task Run(SqlitePortfolioRepository repository)
    {
        Console.WriteLine("Stock Portfolio Simulator");
        Portfolio? portfolio = await repository.LoadAsync();

        if (portfolio == null)
        {
            portfolio = CreatePortfolioFromInput();
        }
        else
        {
            Console.WriteLine("Saved portfolio loaded.");
        }

        while (true)
        {
            DisplayMenu();
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleBuy(portfolio);
                    break;

                case "2":
                    HandleSell(portfolio);
                    break;

                case "3":
                    DisplayPortfolio(portfolio);
                    break;

                case "4":
                    DisplayTransactionHistory(portfolio);
                    break;

                case "5":
                    await repository.SaveAsync(portfolio);
                    Console.WriteLine("Portfolio saved.");
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
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
        string symbol = Console.ReadLine()!.Trim().ToUpperInvariant();

        while (string.IsNullOrWhiteSpace(symbol))
        {
            Console.Write("Stock symbol cannot be blank. Enter stock symbol: ");
            symbol = Console.ReadLine()!.Trim().ToUpperInvariant();
        }

        Console.Write("Enter company name: ");
        string name = Console.ReadLine()!.Trim();
        Asset asset = new Asset(symbol, name);
        return asset;
    }

    private static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine();
        Console.WriteLine(portfolio);

        foreach (Holding holding in portfolio.Holdings)
        {
            Console.WriteLine($"- {holding}");
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
        while (true)
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
        Console.WriteLine("4. View transaction history");
        Console.WriteLine("5. Exit");
        Console.WriteLine();
        Console.Write("Choose an option: ");
    }

    private static void HandleBuy(Portfolio portfolio)
    {
        Asset asset = CreateAssetFromInput();
        decimal quantity = ReadPositiveDecimal("Enter quantity to buy: ");
        decimal purchasePrice = ReadPositiveDecimal("Enter purchase price: ");
        TradeResult result = portfolio.TryBuy(asset, quantity, purchasePrice);
        Console.WriteLine();

        switch (result)
        {
            case TradeResult.Success:
                Console.WriteLine("Purchase successful!");
                DisplayPortfolio(portfolio);
                break;

            case TradeResult.InvalidQuantity:
                Console.WriteLine("Purchase failed: invalid quantity.");
                break;

            case TradeResult.InvalidPrice:
                Console.WriteLine("Purchase failed: invalid price.");
                break;

            case TradeResult.InsufficientCash:
                Console.WriteLine("Purchase failed: insufficient cash.");
                break;

            case TradeResult.AssetNotFound:
                Console.WriteLine("Purchase failed: invalid asset.");
                break;
        }
    }

    private static void HandleSell(Portfolio portfolio)
    {
        Console.Write("Enter stock symbol: ");
        string symbol = Console.ReadLine()!.Trim().ToUpperInvariant();

        decimal quantity = ReadPositiveDecimal("Enter quantity to sell: ");
        decimal salePrice = ReadPositiveDecimal("Enter sale price: ");

        TradeResult result = portfolio.TrySell(symbol, quantity, salePrice);
        Console.WriteLine();

        switch (result)
        {
            case TradeResult.Success:
                Console.WriteLine("Sale successful!");
                DisplayPortfolio(portfolio);
                break;

            case TradeResult.InvalidQuantity:
                Console.WriteLine("Sale failed: invalid quantity.");
                break;

            case TradeResult.InvalidPrice:
                Console.WriteLine("Sale failed: invalid price.");
                break;

            case TradeResult.AssetNotFound:
                Console.WriteLine("Sale failed: asset not found.");
                break;

            case TradeResult.InsufficientShares:
                Console.WriteLine("Sale failed: insufficient shares.");
                break;
        }
    }

    private static void DisplayTransactionHistory(Portfolio portfolio)
    {
        Console.WriteLine();

        if (portfolio.Transactions.Count == 0)
        {
            Console.WriteLine("No transactions have been recorded yet.");
            return;
        }

        foreach (Transaction transaction in portfolio.Transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}
