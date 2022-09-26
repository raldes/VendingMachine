using VendingMachine.App.Models;

namespace VendingMachine.App.Dtos
{
    public class DevolutionResponse
    {
        public ICollection<CoinAmount>? Devolution { get; set; }
    }
}
