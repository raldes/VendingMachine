using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface ICoinsAmountRepository
    {
        ICollection<CoinAmount> Get();
    }
}