using System;
using System.Threading.Channels;
using VendingMachine.App.CommandHandlers;
using VendingMachine.App.Contracts;
using VendingMachine.App.Models;
using VendingMachine.Data;

namespace VendingMachine.UI
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        private static IMachine _machine;
        private static ICoinsRepository _coinsRepository;

        private static bool _cancel;
        private static bool _sold;

        static void Main(string[] args)
        {
            //pedir machine al factory:
            //_machine = _serviceProvider.GetService<IMachine>();
            //_coinsRepository = _serviceProvider.GetService<ICoinsRepository>();

            _coinsRepository = new CoinsRepository();

            _machine = new Machine(new ProductsRepository(), new CoinsAmountRepository(_coinsRepository), _coinsRepository);

            Menu();
        }

        static void Menu()
        {
            PrintHeader();

            while (true)
            {
                DisplayStockProducts();

                _sold = false;
                _cancel = false;

                while (!_sold && !_cancel)
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
                        Cancel();
                        continue;
                    }

                    if (_machine.Coins.Select(c => c.Code).Contains(lowInput))
                    {
                        EnterCoinInput(lowInput);
                        continue;
                    }

                    if (_machine.Products.Select(x => x.Key).Contains(lowInput))
                    {
                        SelectProductInput(lowInput);
                    }
                }
            }

            static void DisplayStockProducts()
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

            static void EnterCoinInput(string input)
            {
                var coin = _machine.Coins.First(c => c.Code == input);

                var enterCoinCommandHandler = new EnterCoinCommandHandler(_machine);

                enterCoinCommandHandler.Handle(new CoinAmount(coin, 1));
            }

            static void SelectProductInput(string lowInput)
            {
                if (_machine.Products.Select(x => x.Key).Contains(lowInput))
                {
                    var product = _machine.Products.First(c => c.Key == lowInput);

                    var moneyDifference = _machine.Wallet.TotalValue - product.Price;

                    if (moneyDifference >= 0)
                    {
                        Select(product);
                    }
                    else
                    {
                        var debMoney = Math.Abs(moneyDifference).ToString("0.00");
                        Console.WriteLine($"Not enough money. Enter {debMoney} euros more.");
                    }
                }
            }

            static bool Select(Product product)
            {
                var selectProductCommandHandler = new SelectProductCommandHandler(_machine);
                var returnCoinAmount = selectProductCommandHandler.Handle(product);
                if (returnCoinAmount == null)
                {
                    //the change have not solution.Cancel operation:
                    Console.WriteLine($"Sorry, I have not money to change.");

                    Cancel();
                    return false;
                }

                Console.WriteLine($"Take your product: {product.Code}");

                if (returnCoinAmount.Any())
                {
                    var coinsToReturn = returnCoinAmount.Select(rc => rc.ToString());

                    var toReturn = String.Join(" | ", coinsToReturn);

                    Console.WriteLine($"Take your change: {toReturn}");
                }

                ClearConsole();

                _sold = true;

                return true;
            }

            static void Cancel()
            {
                var cancelCommandHandler = new CancelCommandHandler(_machine);
                var coinsToReturn = cancelCommandHandler.Handle();

                Console.WriteLine($"Cancelled");

                if (coinsToReturn.Any())
                {
                    var toReturn = String.Join(" | ", coinsToReturn);

                    Console.WriteLine($"Please take your money: {toReturn}");
                }

                _cancel = true;

                ClearConsole();
            }

            static void ClearConsole()
            {
                Console.WriteLine("Enter when ready");
                Console.ReadLine();
                Console.Clear();
            }

            static void ShowOptions()
            {
                var coinOptions = String.Join("|", _machine.Coins.Select(c => c.Code));
                Console.WriteLine($"Deposited money = {_machine.Wallet.TotalValue.ToString("0.00")}");
                Console.WriteLine($"Enter Coin {coinOptions} Or Select Product [Key] Or Cancel [Q]");
            }

            static void PrintHeader()
            {
                Console.WriteLine("Vending machine...");
            }
        }
    }
}


