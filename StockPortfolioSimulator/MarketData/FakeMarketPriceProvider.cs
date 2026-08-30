/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData;

public class FakeMarketPriceProvider : IMarketPriceProvider
{
    public Task<decimal> GetCurrentPrice(Asset asset)
    {
        return Task.FromResult(100m);
    }
}