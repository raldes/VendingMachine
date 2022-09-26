using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface ICoinsAmountGetRepository
    {
        ICollection<CoinAmount> Get();
    }
}