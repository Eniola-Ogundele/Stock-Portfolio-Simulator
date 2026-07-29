/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele
 * Licensed not yet decided
 */

using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public interface IMarketPriceProvider
{
    // A real market price provider will retrieve prices from an external API.
    // Network requests can take time, so this interface returns a Task.
    // This allows the caller to await the result without blocking the application.
    Task<decimal> GetCurrentPrice(Asset asset);
}