using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.Data
{
    public class CoinsAmountRepository : ICoinsAmountRepository
    {
        private readonly ICollection<CoinAmount> _coinsAmount = new List<CoinAmount>();
        private readonly ICoinsRepository _coinsRepository;


        public CoinsAmountRepository(ICoinsRepository coinsRepository)
        {
            _coinsRepository = coinsRepository;
            LoadCoinsAmount();
        }

        public ICollection<CoinAmount> Get()
        {
            return _coinsAmount;
        }

        private void LoadCoinsAmount()
        {
            var tens = new CoinAmount(_coinsRepository.GetByCode("10c"), amount: 100);
            _coinsAmount.Add(tens);

            var twentys = new CoinAmount(_coinsRepository.GetByCode("20c"), amount: 100);
            _coinsAmount.Add(twentys);

            var fittys = new CoinAmount(_coinsRepository.GetByCode("50c"), amount: 100);
            _coinsAmount.Add(fittys);

            var euros = new CoinAmount(_coinsRepository.GetByCode("1e"), amount: 100);
            _coinsAmount.Add(euros);
        }
    }
}