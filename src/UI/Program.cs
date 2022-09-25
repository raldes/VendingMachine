using System;
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

        static void Main(string[] args)
        {
            //pedir machine al factory:
            //_machine = _serviceProvider.GetService<IMachine>();
            //_coinsRepository = _serviceProvider.GetService<ICoinsRepository>();

            _coinsRepository = new CoinsRepository();

            _machine = new Machine(new ProductsRepository(), new CoinsAmountRepository(_coinsRepository), _coinsRepository);

            Show();

        }

        static void Show()
        {
            PrintHeader();

            var productKeys = _machine.Products.Select(x => x.Key);
            var coinNames = _machine.Coins.Select(c => c.Code);

            var coinOptions = String.Join("|", coinNames);

            while (true)
            {
                DisplayRemainProducts();

                bool sold = false;
                bool cancel = false;

                while (!sold && !cancel)
                {
                    Console.WriteLine($"Deposited money = {_machine.Wallet.TotalValue.ToString("0.00")}");
                    Console.WriteLine($"Enter Coin {coinOptions} Or Select Product [Key] Or Cancel [Q]");
                    string? input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }

                    var lowInput = input.ToLower();

                    if (lowInput == "q")
                    {
                        var cancelCommandHandler = new CancelCommandHandler(_machine);

                        var coinsToReturn = cancelCommandHandler.Handle();

                        Console.WriteLine($"Cancelled");

                        if (coinsToReturn.Any())
                        {
                            var toReturn = String.Join(" | ", coinsToReturn);

                            Console.WriteLine($"Please take your money: {toReturn}");
                        }

                        ClearConsole();

                        cancel = true;

                        continue;
                    }

                    if (coinNames.Contains(lowInput))
                    {
                        var coin = _machine.Coins.First(c => c.Code == input);

                        var enterCoinCommandHandler = new EnterCoinCommandHandler(_machine);
                        enterCoinCommandHandler.Handle(new CoinAmount(coin, 1));

                        //Console.WriteLine($"Coin entered: {coin.Code}, Balance: {_machine.Wallet.TotalValue} ");

                        continue;
                    }

                    if (productKeys.Contains(lowInput))
                    {
                        var product = _machine.Products.First(c => c.Key == input);

                        var moneyDifference = _machine.Wallet.TotalValue - product.Price;

                        if (moneyDifference >= 0)
                        {
                            var selectProductCommandHandler = new SelectProductCommandHandler(_machine);

                            var returnCoinAmount = selectProductCommandHandler.Handle(product);

                            Console.WriteLine($"Take your product: {product.Code}");

                            sold = true;

                            if (returnCoinAmount.Any())
                            {
                                var coinsToReturn = returnCoinAmount.Select(rc => rc.ToString());

                                var toReturn = String.Join(" | ", coinsToReturn);

                                Console.WriteLine($"Take your change: {toReturn}");
                            }

                            ClearConsole();
                        }
                        else
                        {
                            var debMoney = Math.Abs(moneyDifference).ToString("0.00");
                            Console.WriteLine($"Not enough money. Enter {debMoney} euros more.");
                            continue;
                        }
                    }
                }
            }

            static void ClearConsole()
            {
                Console.WriteLine("Enter when ready");
                Console.ReadLine();
                Console.Clear();
            }

            static void PrintHeader()
            {
                Console.WriteLine("Vending machine...");
            }

            static void DisplayRemainProducts()
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
        }
    }
}


