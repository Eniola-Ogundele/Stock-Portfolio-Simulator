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

//////////////////////////////////////////////////////////////////////////////////////////////
// Choose which market price provider the application should use.
//
// We do not have a DI container yet, so we create and connect
// the application's dependencies manually.
//
// FinnhubMarketPriceProvider retrieves a real price from the internet.
IMarketPriceProvider marketPriceProvider = new FinnhubMarketPriceProvider(httpClient);

// FakeMarketPriceProvider can be used instead when testing.
// It returns the predictable price 100m without making an API request.
// IMarketPriceProvider marketPriceProvider = new FakeMarketPriceProvider();
//////////////////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("Stock Portfolio Simulator");


var apple = new Asset("AAPL", "Apple"); // Asset to retrieve a price for

// Ask the selected market price provider for Apple's price.
decimal price = await marketPriceProvider.GetCurrentPrice(apple);
Console.WriteLine($"Current price of {apple}: ${price}");