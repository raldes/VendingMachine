# Vending Machine Solution

## Versions:
- 2022.09.26. First development.
- 2022.09.28. Custom Exceptions. Machine Unit Test (method OnSaleProduct)

The solution is a .Net 6 console application.

## Features

- IHostedService application.
- Dependency Injection (.Net native).
- Configuration via app settings and environment variables.
- Logging with Serilog.
- Commands and command handlers, using Mediatr.
- Query Repositories.
- Layers: UI, Application, Infrastructure, and a Testing project.

### UI

- Console controller class:  
    - loop of console-based UI.
    - get the inputs, send the commands (Mediatr)
    - show machine status
    - try catch exception control.

### Application

- Domain models: 
    - The Machine class models the behavior of Vending Machine. It manages Wallet and Products stock.
    - The Wallet class manages input coins, balance, and coins for change (when change is required) or cancelation. 
        - To get the coins (to make change), it implements a greedy algorithm ("an algorithm that follows the problem-solving heuristic of making the locally optimal choice at each stage").
        - If no combination of coins is found, the machine cancels the operation and returns the entered coins to the user.
    - The Product record models the products in stock.
    - The Coin and CoinAmount records model the coins in the wallet and the coin for change or devolution.

- Command and command handlers:
    - EnterCoinCommandHandler.
    - SelectProductCommandHandler.
    - CancelCommandHandler.

- Contracts:
    - Interfaces IMachine and IWallet
    - Interfaces for Get Repositories (IProductsGetRepository, ICoinsGetRepository, ICoinsAmountGetRepository)

- Dtos with response records.

### Infrastructure

- Get Repositories:  implements the get queries (fake data)
    - ProductsGetRepository. 
    - CoinsGetRepository.
    - CoinsAmountGetRepository.

### Deployment

The application has a Publish for deployment in a folder and tests its functionality.

2022.09.26
