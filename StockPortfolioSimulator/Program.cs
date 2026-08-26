using StockPortfolioSimulator;
using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.Persistence;

SqlitePortfolioRepository repository = 
    new SqlitePortfolioRepository("Data Source=portfolio.db");

await repository.InitializeAsync();

IMarketPriceProvider marketPriceProvider = new FakeMarketPriceProvider();

await ConsolePortfolioApp.Run(repository, marketPriceProvider);