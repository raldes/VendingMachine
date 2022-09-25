using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.App.CommandHandlers
{
    public class SelectProductCommandHandler
    {
        private readonly IMachine _machine;

        public SelectProductCommandHandler(IMachine machine)
        {
            _machine = machine;
        }

        public ICollection<CoinAmount> Handle(Product product)
        {
            return _machine.SellProduct(product);
        }
    }
}
