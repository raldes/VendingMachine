using VendingMachine.App.Models;

namespace VendingMachine.App.Contracts
{
    public interface IMachine
    {
        ICollection<Product> Products { get; }
        MachineState State { get; }
        MachineWallet Wallet { get; }
        ICollection<Coin> Coins { get; }

        void MachineStarted();
        void SetCancelledState();
        void SetInsuficientMoneyLoadedState();
        void SetMoneyChangeRequeridState();
        void SetProductSelectedState();
        void SetProductsLoadedState();
        void SetSuficientMoneyLoadedState();

        ICollection<CoinAmount> SellProduct(Product product);
        ICollection<CoinAmount> CancelOperation();

    }
}