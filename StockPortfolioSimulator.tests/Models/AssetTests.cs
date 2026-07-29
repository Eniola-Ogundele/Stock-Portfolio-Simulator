/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Kyle Giver
 * Licensed not yet decided
 */


using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.tests.Models;

public class AssetTests
{
    [Fact]
    public void Constructor_SetsSymbolAndName()
    {
        // Arrange the test data
        const string symbol = "MSFT";
        const string name = "Microsoft";

        // Act on the object being tested
        var asset = new Asset(symbol, name);

        // Assert the outcome is as expected
        Assert.Equal(symbol, asset.Symbol);
        Assert.Equal(name, asset.Name);
    }

    [Fact]
    public void Name_CannotBeChangedAfterConstruction()
    {
        // Arrange
        // Reflection lets us inspect the property because trying to assign
        // asset.Name directly would prevent the test project from compiling.
        var nameProperty = typeof(Asset).GetProperty(nameof(Asset.Name));

        // Assert
        Assert.NotNull(nameProperty);
        Assert.False(nameProperty!.CanWrite);
        Assert.Null(nameProperty.SetMethod);
    }
}