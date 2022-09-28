using System.Drawing;
using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface IWallet
    {
        decimal Balance { get; }
        List<CoinAmount> ChangeCoinsAmount { get; set; }
        ICollection<CoinAmount> DepositedCoinsAmount { get; set; }
        ICollection<Coin> Coins { get; set; }
        void AddCoinAmount(CoinAmount coinAmount);
        void ClearDeposited();
        ICollection<CoinAmount> OnSoldProduct(Product product);
    }
}