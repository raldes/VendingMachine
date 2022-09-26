
using VendingMachine.App.Contracts;

namespace VendingMachine.App.Models
{
    public class Wallet : IWallet
    {
        private readonly ICoinsAmountGetRepository _coinsAmountRepository;
        private readonly ICoinsGetRepository _coinsRepository;

        public Wallet(ICoinsAmountGetRepository coinsAmountRepository, ICoinsGetRepository coinsRepository)
        {
            _coinsAmountRepository = coinsAmountRepository;
            _coinsRepository = coinsRepository; 

            Start();
        }

        private void Start()
        {
            ClearDeposited();

            _changeCoinsAmount = _coinsAmountRepository.Get().ToList();
            _coins = _coinsRepository.Get().ToList();
        }

        private ICollection<Coin> _coins;
        public ICollection<Coin> Coins
        {
            get { return _coins; }
            set { _coins = value; }
        }

        private ICollection<CoinAmount> _depositedCoinsAmount = new List<CoinAmount>();
        public ICollection<CoinAmount> DepositedCoinsAmount
        {
            get { return _depositedCoinsAmount; }
            set { _depositedCoinsAmount = value; }
        }

        private List<CoinAmount> _changeCoinsAmount = new List<CoinAmount>();
        public List<CoinAmount> ChangeCoinsAmount
        {
            get { return _changeCoinsAmount; }
            set { _changeCoinsAmount = value; }
        }

        public decimal Balance
        {
            get { return _depositedCoinsAmount.Sum(c => c.Amount * c.Coin.Value); }
        }

        public void MoveDepositedCoinsToChangeCoins()
        {
            foreach (var coin in _depositedCoinsAmount)
            {
                var changeCoinAmount = _changeCoinsAmount.FirstOrDefault(c => c.Coin.Code == coin.Coin.Code);
                if (changeCoinAmount == null)
                {
                    _changeCoinsAmount.Add(coin);
                }
                else
                {
                    changeCoinAmount.Amount += coin.Amount;
                }
            }

            _depositedCoinsAmount.Clear();
        }

        public decimal GetExtraDepositedValue(decimal price)
        {
            return Balance - price;
        }

        public ICollection<CoinAmount> GetChangeCoinAmmount(decimal value)
        {
            var returnCoins = new List<CoinAmount>();

            var sortedCoins = _changeCoinsAmount.Where(ca => ca.Amount > 0).OrderByDescending(c => c.Coin.Value);

            var rest = value;
            while (rest > 0)
            {
                //get greather coin <= rest:
                var selectedCoin = sortedCoins.Where(ca => ca.Amount > 0).FirstOrDefault(c => c.Coin.Value <= rest);
                if (selectedCoin == null)
                {
                    //todo: have not money to return. Add to Return the minimal value until the rest is satified.
                    return null;
                }

                rest -= selectedCoin.Coin.Value;
                selectedCoin.Amount -= 1;
                returnCoins.Add(new CoinAmount(selectedCoin.Coin, 1));
            }

            return returnCoins;
        }

        public void ClearDeposited()
        {
            _depositedCoinsAmount.Clear();
        }
        
        public void LoadCoinAmounts()
        {
            _changeCoinsAmount = _coinsAmountRepository.Get().ToList();
        }

        public void AddCoinAmount(CoinAmount coinAmount)
        {
            _depositedCoinsAmount.Add(coinAmount);
        }
    }
}
