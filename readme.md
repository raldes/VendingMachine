# Vending Machine Solution

The solution is .Net 6 console application.

## Features

- IHostedService application.
- Dependency Inyection (.Net native).
- Configuration via appsetting and environment variables.
- Logging with Serilog.
- Commands and command handlers, using Mediatr.
- Query Repositories.
- Three layers: UI, Application and Infraestructure.

### UI 
- Console controller class:  
    - loop of console based UI.
    - get the inputs, send the commands (Mediatr)
    - show machine status
    - try catch exception control.

### Application

- Domain models: 

    The IMachine Machine class models the behavior of Vending Machine. It manages Wallet and Product stock.

    The Wallet class manages inputed coins, balance and coins for change (when change is requerid) or cancelation. 
        - When change is requerid, the get the coins to return, it implements a greedy algorithm: 
            "is an algorithm that follows the problem-solving heuristic of making the locally optimal choice at each stage"
        - If not any combination of coins is found, the machine cancel the operation and return the entered money to the user.

    The Product record models the products in stock.

    The Coin and CoinAmount records model the coins in the wallet and the coin for change or devolution.

- Command and command handlers:
    - EnterCoinCommandHandler.
    - SelectProductCommandHandler.
    - CancelCommandHandler.

- Contracts:
    - Interface IMachine
    - Interfaces for Get Repositories (IProductsGetRepository, ICoinsGetRepository, ICoinsAmountGetRepository)

- Dtos with response records.


### Infrastructure

- Get Repositories:  implements the get queries (fake data)
    - ProductsGetRepository. 
    - CoinsGetRepository.
    - CoinsAmountGetRepository.