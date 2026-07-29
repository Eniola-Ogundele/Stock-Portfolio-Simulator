/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele
 * Licensed not yet decided
 */

using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public class FakeMarketPriceProvider : IMarketPriceProvider
{
    // The method returns Task<decimal> instead of decimal because a real
    // market price provider will need to wait for a response from an API.
    // The caller can await the Task and receive the decimal price when ready.
    public Task<decimal> GetCurrentPrice(Asset asset)
    {
        // The fake provider has no network request to wait for,
        // so it wraps the immediate value in a completed Task.
        return Task.FromResult(100m);
    }
}