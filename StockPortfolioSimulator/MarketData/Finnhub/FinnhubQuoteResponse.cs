/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Kyle Givler
 * Licensed not yet decided
 */

// API documentation: https://finnhub.io/docs/api
// Quote endpoint: https://finnhub.io/docs/api/quote
//
// Example response:
// {
//     "c": 394.68,
//     "d": 1.33,
//     "dp": 0.3381,
//     "h": 395.82,
//     "l": 388.74,
//     "o": 395.34,
//     "pc": 393.35,
//     "t": 1785338864
// }

using System.Text.Json.Serialization;

namespace StockPortfolioSimulator.MarketData.Finnhub;

public class FinnhubQuoteResponse
{
    // Finnhub uses very short JSON property names such as "c" and "dp".
    // JsonPropertyName tells System.Text.Json which API property should be
    // mapped to each clearly named C# property.
    [JsonPropertyName("c")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("d")]
    public decimal Change { get; set; }

    [JsonPropertyName("dp")]
    public decimal PercentChange { get; set; }

    [JsonPropertyName("h")]
    public decimal HighPrice { get; set; }

    [JsonPropertyName("l")]
    public decimal LowPrice { get; set; }

    [JsonPropertyName("o")]
    public decimal OpenPrice { get; set; }

    [JsonPropertyName("pc")]
    public decimal PreviousClosePrice { get; set; }

    [JsonPropertyName("t")]
    public long Timestamp { get; set; }
}