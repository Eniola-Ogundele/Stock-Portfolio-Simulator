/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele, Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.MarketData.Finnhub;
using StockPortfolioSimulator.Models;

//////////////////////////////////////////////////////////////////////////////////////////////

var options = new FinnhubOptions
{
    // TEMPORARY LOCAL TESTING ONLY.
    // Do not commit the real key.
    ApiKey = "YOUR_LOCAL_API_KEY_BUT_ALWAYS_REMOVE_BEFORE_CHECKING_IN_TO_GIT",
};

// HttpClient is used to send requests to the Finnhub API.
using var httpClient = new HttpClient
{
    // Relative request URLs will be added to this base URL.
    BaseAddress = new Uri(options.BaseUrl)
};

// Send the API key in a request header instead of placing it in the URL.
httpClient.DefaultRequestHeaders.Add(
    "X-Finnhub-Token",
    options.ApiKey);

// We do not have a DI container yet, so we create and connect
// the application's dependencies manually.
IMarketPriceProvider marketPriceProvider = new FinnhubMarketPriceProvider(httpClient);

// We could also use the FakeMarketPriceProvider for local testing instead of the real Finnhub provider.
// Returning 100 as a fake price for every asset.
// IMarketPriceProvider marketPriceProvider = new FakeMarketPriceProvider();

//////////////////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("Stock Portfolio Simulator");


var apple = new Asset("AAPL", "Apple"); // Asset to retrieve a price for

// Ask the real Finnhub provider for Apple's current market price.
decimal price = await marketPriceProvider.GetCurrentPrice(apple);
Console.WriteLine($"Current price of {apple}: ${price}");