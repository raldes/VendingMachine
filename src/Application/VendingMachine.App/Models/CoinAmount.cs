
namespace VendingMachine.App.Models
{
    public record CoinAmount
    {
        public Coin Coin { get; init; }
        public int Amount { get; set; }

        public CoinAmount(Coin coin, int amount)
        {
            Coin = coin;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Coin: {Coin.Code} Amount: {Amount}";
        }
    }
}
