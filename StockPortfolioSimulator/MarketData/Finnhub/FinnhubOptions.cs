/*
* Stock-Portfolio-Simulator
* Copyright (c) 2026 Kyle Givler
* License not yet decided
*/

// Note: IOptions<FinnhubOptions> has not been wired up yet.

namespace StockPortfolioSimulator.MarketData.Finnhub;

public class FinnhubOptions
{
    public const string SectionName = "Finnhub";

    // TEMPORARY: A real API key may be placed here for local testing
    // until configuration and IOptions<FinnhubOptions> are wired up.
    //
    // Remove the key before committing or pushing any changes.
    // Anyone who obtains the key could use your Finnhub account
    // and consume its API request quota.
    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "https://finnhub.io/api/v1/";
}