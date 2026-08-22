using StockPortfolioSimulator;
using StockPortfolioSimulator.Persistence;

SqlitePortfolioRepository repository = new SqlitePortfolioRepository("Data Source=portfolio.db");

await repository.InitializeAsync();

ConsolePortfolioApp.Run();
