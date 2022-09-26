using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface ICoinsGetRepository
    {
        ICollection<Coin> Get();
        Coin GetByCode(string name);
    }
}