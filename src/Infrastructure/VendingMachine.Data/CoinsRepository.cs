using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.Data
{
    public class CoinsRepository : ICoinsRepository
    {
        private readonly ICollection<Coin> _coins = new List<Coin>();

        public CoinsRepository()
        {
            LoadCoins();
        }

        public ICollection<Coin> Get()
        {
            return _coins;
        }
        
        public Coin GetByCode(string code)
        {
            return _coins.SingleOrDefault(c => c.Code == code);
        }
      
        private void LoadCoins()
        {
            var ten = new Coin(code: "10c", value: 0.1M);
            _coins.Add(ten);

            var twenty = new Coin(code: "20c", value: 0.2M);
            _coins.Add(twenty);

            var fitty = new Coin(code: "50c", value: 0.5M);
            _coins.Add(fitty);

            var euro = new Coin(code: "1e", value: 1.0M);
            _coins.Add(euro);
        }
    }
}