using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VendingMachine.App.Commands;
using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.UI
{
    public class Controller : BackgroundService
    {
        private readonly ILogger<Controller> _logger;
        private readonly IMachine _machine;
        private readonly IMediator _mediator;

        private bool _cancel;
        private bool _sold;

        public Controller(
            ILogger<Controller> logger,
            IMachine machine,
            IMediator mediator)
        {
            _logger = logger;
            _machine = machine;
            _mediator = mediator; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await TaskProccess();
            }
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service estarted");

            return base.StartAsync(stoppingToken);

        }
        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service stoped");

            return base.StopAsync(stoppingToken);
        }

        private async Task TaskProccess()
        {
            DisplayStockProducts();

            _sold = false;
            _cancel = false;

            try
            {
                while (!(_sold || _cancel))
                {
                    ShowOptions();

                    string? input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }

                    var lowInput = input.ToLower();

                    if (lowInput == "q")
                    {
                        await Cancel();
                        continue;
                    }

                    if (_machine.Coins.Select(c => c.Code).Contains(lowInput))
                    {
                        await EnterCoinInput(lowInput);
                        continue;
                    }

                    if (_machine.Products.Select(x => x.Key).Contains(lowInput))
                    {
                        await SelectProductInput(lowInput);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Machine Error. Cancel operation.");

                _logger.LogError(ex.ToString());

                await Cancel();

                throw;
            }
        }

        private void DisplayStockProducts()
        {
            Console.WriteLine($"\n{"Key"} \t {"Product".PadRight(18)} {"Price".PadLeft(7)} \t {"Stock".PadRight(4)}");
            Console.WriteLine("----------------------------------------------");

            foreach (var product in _machine.Products)
            {
                if (product.Portions > 0)
                {
                    var portions = product.Portions.ToString();
                    string code = product.Code;
                    string price = product.Price.ToString("0.00");
                    Console.WriteLine($"{product.Key} \t {code.PadRight(20)} {price} \t {portions}");
                }
            }

            Console.WriteLine();
        }

        private async Task EnterCoinInput(string input)
        {
            var coin = _machine.Coins.First(c => c.Code == input);

            var command = new EnterCoinCommand
            {
                CoinAmount = new CoinAmount(coin, 1)
            };

            _mediator.Send(command);
        }

        private async Task SelectProductInput(string lowInput)
        {
            if (_machine.Products.Select(x => x.Key).Contains(lowInput))
            {
                var product = _machine.Products.First(c => c.Key == lowInput);

                var moneyDifference = _machine.Wallet.Balance - product.Price;

                if (moneyDifference >= 0)
                {
                    await Select(product);
                }
                else
                {
                    var debMoney = Math.Abs(moneyDifference).ToString("0.00");

                    Console.WriteLine($"Not enough money. Enter {debMoney} euros more.");
                }
            }
        }

        private async Task<bool> Select(Product product)
        {
            var command = new SelectProductCommand
            {
                Product = product
            };

            var response = await _mediator.Send(command);
            if (response == null)
            {
                //the change have not solution.Cancel operation:
                Console.WriteLine($"Sorry, I have not money to change.");

                await Cancel();
                return false;
            }

            Console.WriteLine($"Take your product: {product.Code}");

            if (response.Devolution != null && response.Devolution.Any())
            {
                var coinsToReturn = response.Devolution.Select(rc => rc.ToString());

                var toReturn = String.Join(" | ", coinsToReturn);

                Console.WriteLine($"Take your change: {toReturn}");
            }

            ClearConsole();

            _sold = true;

            return true;
        }

        private async Task Cancel()
        {
            var response = await _mediator.Send(new CancelCommand());

            Console.WriteLine($"Cancelled");

            if (response != null && response.Devolution.Any())
            {
                var toReturn = String.Join(" | ", response.Devolution);

                Console.WriteLine($"Please take your money: {toReturn}");
            }

            _cancel = true;

            ClearConsole();
        }

        private void ClearConsole()
        {
            Console.WriteLine("Enter when ready");
            Console.ReadLine();
            Console.Clear();
        }

        private void ShowOptions()
        {
            var coinOptions = String.Join("|", _machine.Coins.Select(c => c.Code));
            Console.WriteLine($"Deposited money = {_machine.Wallet.Balance.ToString("0.00")}");
            Console.WriteLine($"Enter one coin: [{coinOptions}] Or Select one product [Key] Or Cancel [Q]");
        }
    }
}


