using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Contracts;

namespace VendingMachine.App.Models
{
    public class Machine : IMachine
    {
        private ICollection<Product> _products = new List<Product>();
        public ICollection<Product> Products
        {
            get { return _products; }
        }

        private MachineState _state;
        public MachineState State
        {
            get { return _state; }
        }

        private MachineWallet _wallet = new MachineWallet();
        public MachineWallet Wallet
        {
            get { return _wallet; }
        }

        private readonly IProductsRepository _productsRepository;
        private readonly ICoinsRepository _coinsRepository;
        private readonly ICoinsAmountRepository _coinsAmountRepository;

        public Machine(IProductsRepository productsRepository,
            ICoinsAmountRepository coinsAmountRepository,
            ICoinsRepository coinsRepository)
        {
            _productsRepository = productsRepository;
            _coinsAmountRepository = coinsAmountRepository;
            _coinsRepository = coinsRepository;

            MachineStarted();
        }

        public void MachineStarted()
        {
            _state = MachineState.MachineStarted;

            _products = _productsRepository.Get();

            _wallet.Clean();

            _wallet.ChangeCoinsAmount = _coinsAmountRepository.Get().ToList();
        }

        public ICollection<Coin> Coins => _coinsRepository.Get();

        public ICollection<CoinAmount> SellProduct(Product product)
        {
            var valueToReturn = _wallet.GetExtraDepositedValue(product.Price);

            product.RemovePortions(1);

            //move the coins from deposited wallet to change value and empty deposited:
            _wallet.MoveDepositedCoinsToChangeCoins();

            var coinAmountsToReturn =_wallet.GetChangeCoinAmmount(valueToReturn);

            return coinAmountsToReturn;
        }
        
        public ICollection<CoinAmount> CancelOperation()
        {
            var coinAmountsToReturn =_wallet.DepositedCoinsAmount;

            _wallet.Clean();

            return coinAmountsToReturn;
        }

        public void SetProductsLoadedState()
        {
            _state = MachineState.ProductsLoaded;
        }

        public void SetInsuficientMoneyLoadedState()
        {
            _state = MachineState.InsuficientMoneyLoaded;
        }

        public void SetSuficientMoneyLoadedState()
        {
            _state = MachineState.SuficientMoneyLoaded;
        }

        public void SetProductSelectedState()
        {
            _state = MachineState.ProductSelected;
        }

        public void SetMoneyChangeRequeridState()
        {
            _state = MachineState.MoneyChangeRequerid;
        }

        public void SetCancelledState()
        {
            _state = MachineState.Cancelled;
        }

    }
}
