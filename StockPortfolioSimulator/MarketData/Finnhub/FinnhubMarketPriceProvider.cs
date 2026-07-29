/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.Models;
using System.Net.Http.Json;

namespace StockPortfolioSimulator.MarketData.Finnhub;

// This class retrieves real stock prices from the Finnhub API.
// It implements IMarketPriceProvider, so it can be used anywhere
// the application expects a market price provider.
internal class FinnhubMarketPriceProvider : IMarketPriceProvider
{
    // HttpClient sends HTTP requests to Finnhub.
    // It is provided through the constructor instead of being created here,
    // allowing the same HttpClient instance to be reused.
    private readonly HttpClient _httpClient;

    internal FinnhubMarketPriceProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetCurrentPrice(Asset asset)
    {
        // Make the stock symbol safe to include in a URL.
        // For example, special characters will be properly encoded.
        string symbol = Uri.EscapeDataString(asset.Symbol);

        // Send a GET request to the Finnhub quote endpoint.
        // await waits for the response without blocking the application.
        //
        // GetFromJsonAsync also converts the returned JSON into a
        // FinnhubQuoteResponse object.
        FinnhubQuoteResponse? quote = await _httpClient.GetFromJsonAsync<FinnhubQuoteResponse>($"quote?symbol={symbol}");

        // The response is nullable because the API might return no usable data.
        // Stop with a clear error instead of attempting to use a null object.
        if (quote is null)
        {
            throw new InvalidOperationException($"Finnhub did not return a quote for {asset.Symbol}.");
        }

        // Return only the current price required by IMarketPriceProvider.
        return quote.CurrentPrice;
    }
}