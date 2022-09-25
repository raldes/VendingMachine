using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.App.CommandHandlers
{
    public class CancelCommandHandler
    {
        private readonly IMachine _machine;

        public CancelCommandHandler(IMachine machine)
        {
            _machine = machine;
        }

        public ICollection<CoinAmount> Handle()
        {
            var coinsToReturn = _machine.CancelOperation();

            return coinsToReturn;
        }
    }
}
