# Stock-Portfolio-Simulator

Stock Portfolio Simulator is C# console application that simulates stock trading using current market prices from Finnhub. The application allows users to create and manage a virtual investment portfolio without using real money. Portfolio data is stored locally in a SQLite database so it can be loaded again when the application is restarted.

#### Features

* Create a portfolio with a starting cash balance

* Buy stocks using the current Finnhub market price

* Sell stocks using the current Finnhub market price

* Track stock holdings and quantities

* Calculate weighted average purchase prices

* View transaction history

* View current portfolio performance

* Calculate current holdings value

* Calculate total portfolio value

* Calculate unrealized profit/loss

* Store portfolio data locally using SQLite

* Automatically save successful purchases and sales

* Handle failed market-price requests without performing or saving a trade

#### Prerequisites

Before running the application, make sure you have:

* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

* A Finnhub account and API key

###### Getting a Finnhub API Key

The application uses Finnhub to retrieve current market prices.

1. Go to [Finnhub](https://finnhub.io/).

2. Create an account or sign in.

3. Obtain your API key from your Finnhub account.

***Do not commit your API key to Git or push it to GitHub.***

###### Configuration

The application uses `appsettings.json` for general configuration. Your personal Finnhub API key should be stored in a separate `appsettings.Local.json` file. Create `appsettings.Local.json` in the project directory with the following:

```json
{
  "Finnhub": {
    "ApiKey": "YOUR_FINNHUB_API_KEY",
    "BaseUrl": "https://finnhub.io/api/v1/"
  }
}
```

Replace `YOUR_FINNHUB_API_KEY` with your actual Finnhub API key. `appsettings.Local.json` is intended for local configuration and should not be committed to the repository.

#### Running the Application

1. Clone the repository.

2. Create `appsettings.Local.json` and add your Finnhub API key.

3. Open the project in Visual Studio or another compatible .NET development environment.

4. Build the project.

5. Run the application.

When the application starts, it loads an existing saved portfolio if one is available. If no saved portfolio exists, the application asks you to create a new one.

#### Using the Application

The application provides the following menu options:

```text
1. Buy an asset

2. Sell an asset

3. View portfolio

4. View transaction history

5. View portfolio performance

6. Exit
```

#### Buying an Asset

When buying an asset, enter:

* Stock symbol

* Company name

* Quantity to buy

The application then retrieves the current market price from Finnhub automatically. The user does not manually enter the purchase price.

Example:

```text
Enter stock symbol: AAPL

Enter company name: Apple

Enter quantity to buy: 5

Current AAPL price: $232.14

Purchase successful!
```

The purchase is only completed if:

* The market price can be retrieved successfully.

* The market price is valid.

* The requested quantity is valid.

* The portfolio has enough cash.

Successful purchases are automatically saved to the SQLite database.

#### Selling an Asset

When selling an asset, enter:

* Stock symbol

* Quantity to sell

The application retrieves the current market price from Finnhub automatically and uses that price for the simulated sale.

The sale is only completed if:

* The market price can be retrieved successfully.

* The market price is valid.

* The requested quantity is valid.

* The portfolio contains enough shares.

Successful sales are automatically saved to the SQLite database. If the market price cannot be retrieved, the sale is not performed and nothing is saved.

#### Portfolio Performance

Portfolio performance uses current market prices retrieved from Finnhub. For each holding, the application calculates:

###### Cost Basis

The cost basis is calculated as:

```text
Quantity × Average Purchase Price
```

###### Current Value

The current value is calculated as:

```text
Quantity × Current Market Price
```

###### Unrealized Profit/Loss

Unrealized profit/loss is calculated as:

```text
Current Value − Cost Basis
```

The total portfolio value is calculated as:

```text
Cash Balance + Current Value of All Holdings
```

The application also calculates total profit/loss by comparing the current portfolio value with the amount originally invested.

#### Data Storage

Portfolio data is stored locally in a SQLite database named:

```text
portfolio.db
```

The database stores:

* Portfolio cash balance

* Current holdings

* Transaction history

Successful purchases and sales are saved automatically. Failed trades do not modify the portfolio or save anything to the database. When the application is restarted, the saved portfolio is loaded from the SQLite database.

#### Market Data

Current stock prices are retrieved from Finnhub. The application uses the current Finnhub market price when performing simulated purchases and sales. If Finnhub cannot provide a valid market price, the application displays an error message and does not perform the trade.

#### Error Handling

The application handles invalid input and market-data failures without crashing during normal operation. For example, if a market price cannot be retrieved, the application displays:

```text
Unable to retrieve the current market price. Please try again later.
```

In this situation:

* The trade is not performed.

* The portfolio is not modified.

* Nothing is saved for the failed trade.

* The application returns to the menu.

#### Paper Trading Only

This project is a **paper-trading simulator**.

It does not:

* Use real money

* Place real stock orders

* Connect to a brokerage account

* Buy or sell real securities

All balances, holdings, transactions, and profits/losses are simulated.

#### Project Status

This project is currently at its first MVP release. The MVP provides basic portfolio management, simulated buying and selling, local SQLite persistence, current Finnhub market prices, transaction history, and portfolio performance tracking.