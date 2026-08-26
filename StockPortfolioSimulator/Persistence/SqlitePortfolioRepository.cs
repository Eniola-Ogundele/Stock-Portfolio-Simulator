using Dapper;
using Microsoft.Data.Sqlite;
using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Persistence;

internal class PortfolioRow
{
    public decimal CashBalance { get; set; }
}

internal class HoldingRow
{
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal AveragePurchasePrice { get; set; }
}

internal class TransactionRow
{
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
}

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

        try
        {
            string portfolioSql = """
                INSERT INTO Portfolio (Id, CashBalance)
                Values (1, @CashBalance)
                ON CONFLICT(Id) DO UPDATE SET CashBalance = excluded.CashBalance;
                """;

            await connection.ExecuteAsync(portfolioSql, new { portfolio.CashBalance }, transaction);

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


            await transaction.CommitAsync();
        }

        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task<Portfolio?> LoadAsync()
    {
        await using SqliteConnection connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        PortfolioRow? portfolioRow = await connection.QuerySingleOrDefaultAsync<PortfolioRow>("SELECT CashBalance FROM Portfolio WHERE Id = 1;");

        if (portfolioRow == null)
        {
            return null;
        }

        IEnumerable<HoldingRow> holdingRows = await connection.QueryAsync<HoldingRow>("""
            SELECT Symbol, Name, Quantity, AveragePurchasePrice
            FROM Holdings;
            """);

        IEnumerable<TransactionRow> transactionRows = await connection.QueryAsync<TransactionRow>("""
            SELECT Symbol, Name, Type, Quantity, Price, Timestamp
            FROM Transactions
            Order By Timestamp;
            """);

        List<Holding> holdings = new();

        foreach (HoldingRow row in holdingRows)
        {
            Asset asset = new Asset(row.Symbol, row.Name);

            Holding holding = new Holding(asset, row.Quantity, row.AveragePurchasePrice);

            holdings.Add(holding);
        }

        List<Transaction> transactions = new();

        foreach (TransactionRow row in transactionRows)
        {
            Asset asset = new Asset(row.Symbol, row.Name);

            TransactionType type = Enum.Parse<TransactionType>(row.Type);

            Transaction transaction = new Transaction(asset, type, row.Quantity, row.Price, row.Timestamp);

            transactions.Add(transaction);
        }
        return Portfolio.Restore(portfolioRow.CashBalance, holdings, transactions);
    }
}
