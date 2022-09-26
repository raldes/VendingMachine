using VendingMachine.App.Contracts;

namespace VendingMachine.App.Models
{
    public class Machine : IMachine
    {
        private readonly IProductsGetRepository _productsRepository;
        private readonly IWallet _wallet;

        public Machine(IProductsGetRepository productsRepository,
            IWallet wallet)
        {
            _productsRepository = productsRepository;
            _wallet = wallet;

            Start();
        }

        private ICollection<Product> _products = new List<Product>();
        public ICollection<Product> Products
        {
            get { return _products; }
        }

        private void Start()
        {
            _products = _productsRepository.Get();
        }

        public void AddCoinAmount(CoinAmount coinAmount)
        {
            _wallet.AddCoinAmount(coinAmount);
        }


        public ICollection<Coin> Coins => _wallet.Coins;
        
        public decimal GetBalance()
        {
            return _wallet.Balance;
        }

        public ICollection<CoinAmount> SellProduct(Product product)
        {
            var valueToReturn = _wallet.GetExtraDepositedValue(product.Price);
            if(valueToReturn < 0)
            {
                return null;
            }

            var coinAmountsToReturn = _wallet.GetChangeCoinAmmount(valueToReturn);
            if (coinAmountsToReturn == null)
            {
                //the change have not solution (there are not coins to give the change). Cancel operation:
                return null;
            }

            _wallet.MoveDepositedCoinsToChangeCoins();

            product.RemovePortions(1);

            return coinAmountsToReturn;
        }
        
        public ICollection<CoinAmount> CancelOperation()
        {
            var coinAmountsToReturn = new List<CoinAmount>(_wallet.DepositedCoinsAmount);

            _wallet.ClearDeposited();

            return coinAmountsToReturn;
        }
    }
}
