/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

using Microsoft.Extensions.Configuration;
using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.MarketData.Finnhub;
using StockPortfolioSimulator.Persistence;

namespace StockPortfolioSimulator;

public class Program
{
    public static async Task Main()
    {
        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Local.json", optional: true)
                .Build();

            FinnhubOptions options = configuration
                .GetSection(FinnhubOptions.SectionName)
                .Get<FinnhubOptions>()
                ?? throw new InvalidOperationException(
                    "Finnhub configuration is missing.");

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new InvalidOperationException(
                    "Finnhub API key is missing. Please add your API key to appsettings.Local.json.");
            }

            SqlitePortfolioRepository repository =
                new SqlitePortfolioRepository("Data Source=portfolio.db");

            await repository.InitializeAsync();

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl)
            };

            httpClient.DefaultRequestHeaders.Add(
                "X-Finnhub-Token",
                options.ApiKey);

            IMarketPriceProvider marketPriceProvider =
                new FinnhubMarketPriceProvider(httpClient);

            await ConsolePortfolioApp.Run(
                repository,
                marketPriceProvider);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Unable to start the application: {ex.Message}");
            Console.WriteLine();
        }
    }
}