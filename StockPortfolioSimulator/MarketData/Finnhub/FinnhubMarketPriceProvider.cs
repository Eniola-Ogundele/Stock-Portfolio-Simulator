/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.MarketData.Finnhub;

internal class FinnhubMarketPriceProvider : IMarketPriceProvider
{
    private readonly HttpClient _httpClient;

    internal FinnhubMarketPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<decimal> GetCurrentPrice(Asset asset)
    {
        throw new NotImplementedException();
    }
}
