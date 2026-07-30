using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Tests.Models;

public class PortfolioTests
{
    [Fact]
    public void Constructor_SetsCashBalanceAndCreatesEmptyHoldingsList()
    {
        // Arrange
        const decimal startingCash = 10_000m;

        // Act
        var portfolio = new Portfolio(startingCash);

        // Assert
        Assert.Equal(startingCash, portfolio.CashBalance);
        Assert.NotNull(portfolio.Holdings);
        Assert.Empty(portfolio.Holdings);
    }
}