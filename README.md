[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
# My 2 Cents - Project 3 - Revature 
> v2.0.0

# Introduction
My 2 Cents is a Banking Application that focuses on security and investment opportunities for its users. It has an investment portfolio and platform to interface with and it advises the user on certain cryptocurrencies/stocks. It also allows users the ability to buy and sell stocks/cryptocurrencies and has real-time updates from the current values of multiple cryptocurrencies. Additionally, users have access to a budget calculator and advice depending on their budget choices.

# Table of Contents
- [My 2 Cents - Project 3 - Revature](#my-2-cents---project-3---revature)
- [Introduction](#introduction)
- [Table of Contents](#table-of-contents)
- [Features](#features)
  - [Dark Mode](#dark-mode)
  - [Budget Calculator](#budget-calculator)
  - [Investment Platform](#investment-platform)
  - [Investment Porfolio](#investment-porfolio)
  - [Investment Diversity Graph](#investment-diversity-graph)
  - [Identity Server](#identity-server)
  - [Two-Factor Authentication](#two-factor-authentication)
- [Technologies](#technologies)
  - [Backend](#backend)
  - [Others](#others)
- [Getting started](#getting-started)
- [Changelog](#changelog)
- [Contributors](#contributors)
  - [Vijhan Woodley](#vijhan-woodley)
  - [Brian Dawkins](#brian-dawkins)
  - [Jonathon Renaud](#jonathon-renaud)
  - [Terrance Usher](#terrance-usher)
  - [Tuan Anh Nguyen](#tuan-anh-nguyen)
- [License](#license)

# Features
> Added in v2.0.0(included all features in v1.0.1)
## Dark Mode
- Dark mode applied in all components, saved settings in LocalStorage.
## Budget Calculator
- User can input income and expense.
- Calculate budget based on the input data.
- Display chart with budget information.
- Display explanation chart.
## Investment Platform
- Update Crypto, Stock Database with the realtime by using external API from Coingecko(Crypto), YahooFinance(Stock).
- User can see the realtime price, price change, graph,... and decide which one to buy/sell.
- User can buy crypto by the amount of crypto.
- User can buy crypto by the money amount.
- User can buy stock by the amount of stocks/shares.
- User can buy stock by the money amount.
- User can sell crypto by the amount of crypto.
- User can sell crypto by the money amount.
- User can sell stock by the amount of stocks/shares.
- User can sell stock by the money amount.
## Investment Porfolio
- User able to view financial information about investments.
- Page displays total amount of money in investments.
- Investments are separated into crypto and stock.
- Tables are displayed with crypto and stock investments that are in possession.
- Tables are displayed with crypto and stock order history.
## Investment Diversity Graph
- Displays realtime data about Crypto.
- Displays realtime data about Stocks.
- Displays data about user's current checking accounts.
- Displays data about user's current savings accounts.
- Shows user an information alert on hover of an 'i' icon.
- When user clicks on labels, that chart pie slice is toggleable.
## Identity Server
>  Removed Auth0 from previous version.
- Setup ASP.NET Identity.
- Enable Identity, Authentication and Authorization Services for the APIs.
- Setup Authentication Service for Access Token(using JWT).
- Add Middleware for Access Token Manager.
- When user register successful, they have to verify email with the link sent to the register email before login. (Using SendGrid Email to Send Email Confirmation).
- When user login successful, they will have the token which will be stored in their LocalStorage.
- Setup AuthGuard, user have to login successful with the token stored already to access to app components(excluding authentication components).
- HTTP Interceptor setup, the token automatically send whenever have the request to the API when user logged in.
- All the APIs are secured, user can't send the request without the token in the header.
## Two-Factor Authentication
- All users will have Two-Factor Authentication enabled initially when they successfully register.
- SendGrid Account Setup.
- Email Sent with TwoFactorCode.
- User can use the code to authenticate when they login.

# Technologies
## Backend
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [JSON](https://www.json.org/json-en.html)
- [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview)
- [xUnit](https://xunit.net)
- [Moq](https://docs.microsoft.com/en-us/shows/visual-studio-toolbox/unit-testing-moq-framework)
- [SeriLog](https://serilog.net)
- [Azure SQL Server](https://azure.microsoft.com/en-us/services/sql-database/campaign/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web APIs](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity)
- [Swagger](https://swagger.io)
  
## Others
- [Visual Studio Code](https://code.visualstudio.com)
- [DBeaver](https://dbeaver.io)
- [ThunderClient](https://www.thunderclient.com)
- [Git](https://git-scm.com)
- [GitHub](https://github.com)
- [SonarQube](https://www.sonarqube.org)
- [Markdown](https://daringfireball.net/projects/markdown/)
- [Moqups](https://moqups.com)

# Getting started
After you're done setting up from the [SETUP GUIDE](STARTUP.md), you can <code>cd</code> to the API folder.

```cmd
cd ./My2Cents.API/My2Cents.API
```
And run the app by this command line.
```cmd
dotnet run
```

# Changelog
- v.2.0.0

# Contributors
## Vijhan Woodley
- Jasmine Tseng
- Hyunsoo Jeon
- Matthew Chan

## Brian Dawkins
- Abdulkerim Metenea
- Carlos Aviles
- Daniel Brenes

## Jonathon Renaud
- Andrew DeMarco
- Dat Vo
- Nicolas Colorado
- Roberto Sandoval

## Terrance Usher
- Chad Solomon
- Kris Wasserman
- Sid Hinson
  
## Tuan Anh Nguyen

# License
This project is licensed under the [MIT License](LICENSE).

[Back To Top](#my-2-cents---project-3---revature)