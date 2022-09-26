using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface IProductsGetRepository
    {
        ICollection<Product> Get();
    }
}
