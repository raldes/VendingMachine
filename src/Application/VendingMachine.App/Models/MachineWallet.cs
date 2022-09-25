
namespace VendingMachine.App.Models
{
    public class MachineWallet
    {
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

        public decimal TotalValue
        {
            get { return _depositedCoinsAmount.Sum(c => c.Amount * c.Coin.Value); }
        }

        public void MoveDepositedCoinsToChangeCoins()
        {
            foreach (var coin in _depositedCoinsAmount)
            {
                var changeCoinAmount = _changeCoinsAmount.FirstOrDefault(c => c.Coin.Code == coin.Coin.Code);
                if(changeCoinAmount == null)
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
            return TotalValue - price;
        }

        public ICollection<CoinAmount> GetChangeCoinAmmount(decimal value)
        {
            var returnCoins = new List<CoinAmount>();

            var sortedCoins = _changeCoinsAmount.Where(ca => ca.Amount > 0).OrderByDescending(c => c.Coin.Value);

            var rest = value;
            while(rest > 0)
            {
                //get greather coin <= rest:
                var selectedCoin = sortedCoins.Where(ca => ca.Amount > 0).FirstOrDefault(c => c.Coin.Value <= rest);
                if(selectedCoin == null)
                {
                    //have not money to return
                    //todo: algoritmo al reves: dar vuelto mayor. Seleccionar en ciclo las monedas (ordenadas de menor a mayor) que se sumen a newrest hasta que el vuelto se mayor o igual que rest
                    return null;
                }
                 
                rest -= selectedCoin.Coin.Value;
                selectedCoin.Amount -= 1;
                returnCoins.Add(new CoinAmount(selectedCoin.Coin, 1));
            }

            return returnCoins;
        }

        public void Clean()
        {
            _depositedCoinsAmount.Clear();
        }

        public void AddCoinAmount(CoinAmount coinAmount)
        {
            _depositedCoinsAmount.Add(coinAmount);
        }
    }
}
