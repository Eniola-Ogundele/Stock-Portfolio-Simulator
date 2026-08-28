/*
* Stock-Portfolio-Simulator
* Copyright (c) 2026 Kyle Givler
* License not yet decided
*/
using StockPortfolioSimulator.Models;
using System.Net.Http.Json;

namespace StockPortfolioSimulator.MarketData.Finnhub;

internal class FinnhubMarketPriceProvider : IMarketPriceProvider
{
    private readonly HttpClient _httpClient;

    internal FinnhubMarketPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetCurrentPrice(Asset asset)
    {
        string symbol = Uri.EscapeDataString(asset.Symbol);

        HttpResponseMessage response = await _httpClient.GetAsync($"quote?symbol={symbol}");

        response.EnsureSuccessStatusCode();

        FinnhubQuoteResponse? quote = await response.Content.ReadFromJsonAsync<FinnhubQuoteResponse>();

        if (quote is null)
        {
            throw new InvalidOperationException($"Finnhub did not return a quote for {asset.Symbol}.");
        }

        if (quote.CurrentPrice <= 0)
        {
            throw new InvalidOperationException($"Finnhub returned an invalid price for {asset.Symbol}.");
        }

        return quote.CurrentPrice;
    }
}