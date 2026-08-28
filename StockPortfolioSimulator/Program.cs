using Microsoft.Extensions.Configuration;
using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.MarketData.Finnhub;
using StockPortfolioSimulator.Persistence;

namespace StockPortfolioSimulator;

public class Program
{
    public static async Task Main()
    {
        IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false).AddJsonFile("appsettings.Local.json", optional: true).Build();

        FinnhubOptions options =configuration.GetSection(FinnhubOptions.SectionName).Get<FinnhubOptions>()?? throw new InvalidOperationException("Finnhub configuration is missing.");

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new InvalidOperationException("Finnhub API key is missing.");
        }

        SqlitePortfolioRepository repository = new SqlitePortfolioRepository("Data Source=portfolio.db");

        await repository.InitializeAsync();

        HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.BaseUrl)
        };

        httpClient.DefaultRequestHeaders.Add("X-Finnhub-Token",options.ApiKey);

        IMarketPriceProvider marketPriceProvider = new FinnhubMarketPriceProvider(httpClient);

        await ConsolePortfolioApp.Run(repository, marketPriceProvider);
    }
}