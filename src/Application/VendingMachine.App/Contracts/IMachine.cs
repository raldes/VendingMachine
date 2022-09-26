using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface IMachine
    {
        ICollection<Product> Products { get; }
        Wallet Wallet { get; }
        ICollection<Coin> Coins { get; }
        ICollection<CoinAmount> SellProduct(Product product);
        ICollection<CoinAmount> CancelOperation();
    }
}