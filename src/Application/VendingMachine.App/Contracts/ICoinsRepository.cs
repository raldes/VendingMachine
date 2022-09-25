using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface ICoinsRepository
    {
        ICollection<Coin> Get();
        Coin GetByCode(string name);
    }
}