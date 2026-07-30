using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Tests.Models;

public class HoldingTests
{
    [Fact]
    public void Constructor_SetsAssetQuantityAndAveragePurchasePrice()
    {
        // Arrange
        var asset = new Asset("MSFT", "Microsoft");
        const decimal quantity = 5m;
        const decimal averagePurchasePrice = 400m;

        // Act
        var holding = new Holding(asset, quantity, averagePurchasePrice);

        // Assert
        Assert.Same(asset, holding.Asset);
        Assert.Equal(quantity, holding.Quantity);
        Assert.Equal(averagePurchasePrice, holding.AveragePurchasePrice);
    }

    [Fact]
    public void ToString_ReturnsAssetAndQuantity()
    {
        // Arrange
        var asset = new Asset("AAPL", "Apple");
        var holding = new Holding(asset, 3m, 200m);

        // Act
        string result = holding.ToString();

        // Assert
        Assert.Equal("AAPL - Apple | Quantity: 3", result);
    }

    [Fact]
    public void Properties_CannotBeChangedFromOutsideHolding()
    {
        // Arrange
        var assetProperty = typeof(Holding).GetProperty(nameof(Holding.Asset));
        var quantityProperty = typeof(Holding).GetProperty(nameof(Holding.Quantity));
        var averagePriceProperty = typeof(Holding).GetProperty(nameof(Holding.AveragePurchasePrice));

        // Assert
        Assert.NotNull(assetProperty);
        Assert.Null(assetProperty!.SetMethod);

        Assert.NotNull(quantityProperty);
        Assert.NotNull(quantityProperty!.SetMethod);
        Assert.True(quantityProperty.SetMethod!.IsPrivate);

        Assert.NotNull(averagePriceProperty);
        Assert.NotNull(averagePriceProperty!.SetMethod);
        Assert.True(averagePriceProperty.SetMethod!.IsPrivate);
    }
}