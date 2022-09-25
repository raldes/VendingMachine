using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Contracts;
using VendingMachine.App.Models;

namespace VendingMachine.App.CommandHandlers
{
    public class EnterCoinCommandHandler
    {
        private readonly IMachine _machine;

        public EnterCoinCommandHandler(IMachine machine)
        {
            _machine = machine;
        }

        public void Handle(CoinAmount coinAmount)
        {
            //actualizar wallet
            _machine.Wallet.AddCoinAmount(coinAmount);
        }
    }
}
