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
}
