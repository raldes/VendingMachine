using VendingMachine.App.Models;

namespace VendingMachine.App.Dtos
{
    public record DevolutionResponse
    {
        public ICollection<CoinAmount>? Devolution { get; set; }
    }
}
