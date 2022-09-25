using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface IProductsRepository
    {
        ICollection<Product> Get();
    }
}
