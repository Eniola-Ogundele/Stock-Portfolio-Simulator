using Dapper;
using Microsoft.Data.Sqlite;
using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Persistence;

public class SqlitePortfolioRepository
{
    private readonly string _connectionString;

    public SqlitePortfolioRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

public async Task InitializeAsync()
    {
        await using SqliteConnection connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        string sql = """
        CREATE TABLE IF NOT EXISTS Portfolio
        (Id INTEGER PRIMARY KEY, CashBalance TEXT NOT NULL);

        CREATE TABLE IF NOT EXISTS Holdings
        (Id INTEGER PRIMARY KEY AUTOINCREMENT, Symbol TEXT NOT NULL, Name TEXT NOT NULL, Quantity TEXT NOT NULL, AveragePurchasePrice TEXT NOT NULL);

        CREATE TABLE IF NOT EXISTS Transactions
        (Id INTEGER PRIMARY KEY AUTOINCREMENT, Symbol TEXT NOT NULL, Name TEXT NOT NULL, Type TEXT NOT NULL, Quantity TEXT NOT NULL, Price TEXT NOT NULL, Timestamp TEXT NOT NULL);
        """;

        await connection.ExecuteAsync(sql);
    }

public async Task SaveAsync(Portfolio portfolio)
    {
        await using SqliteConnection connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync();

        string portfoliosql = """
        INSERT INTO Portfolio (Id, CashBalance)
        Values (1, @CashBalance)
        ON CONFLICT(Id) DO UPDATE SET CashBalance = excluded.CashBalance;
        """;

        await connection.ExecuteAsync(portfoliosql, new {portfolio.CashBalance}, transaction);

        await connection.ExecuteAsync("DELETE FROM Holdings;", transaction: transaction);

        foreach (Holding holding in portfolio.Holdings)
        {
            string holdingSql = """
            INSERT INTO Holdings (Symbol, Name, Quantity, AveragePurchasePrice)
            VALUES (@Symbol, @Name, @Quantity, @AveragePurchasePrice);
            """;

            await connection.ExecuteAsync(holdingSql, new
            {
                Symbol = holding.Asset.Symbol,
                Name = holding.Asset.Name,
                holding.Quantity,
                holding.AveragePurchasePrice
            }, transaction);
        }

        await connection.ExecuteAsync("DELETE FROM Transactions;", transaction: transaction);

        foreach (Transaction transactionItem in portfolio.Transactions)
        {
            string transactionSql = """
            INSERT INTO Transactions (Symbol, Name, Type, Quantity, Price, Timestamp)
            VALUES (@Symbol, @Name, @Type, @Quantity, @Price, @Timestamp);
            """;

            await connection.ExecuteAsync(transactionSql, new
            {
                Symbol = transactionItem.Asset.Symbol,
                Name = transactionItem.Asset.Name,
                Type = transactionItem.Type.ToString(),
                transactionItem.Quantity,
                transactionItem.Price,
                transactionItem.Timestamp
            }, transaction);
        }
        
        try
        {
            await transaction.CommitAsync();
        }

        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
}
