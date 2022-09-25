using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Exceptions;
using VendingMachine.App.SeedWork;

namespace VendingMachine.App.Models
{
    public class MachineState : Enumeration
    {
        public static MachineState MachineStarted = new MachineState(1, nameof(MachineStarted).ToLowerInvariant());
        public static MachineState ProductsLoaded = new MachineState(2, nameof(ProductsLoaded).ToLowerInvariant());
        public static MachineState CoinLoaded = new MachineState(3, nameof(CoinLoaded).ToLowerInvariant());
        public static MachineState InsuficientMoneyLoaded = new MachineState(4, nameof(InsuficientMoneyLoaded).ToLowerInvariant());
        public static MachineState SuficientMoneyLoaded = new MachineState(5, nameof(SuficientMoneyLoaded).ToLowerInvariant());
        public static MachineState ProductSelected = new MachineState(6, nameof(ProductSelected).ToLowerInvariant());
        public static MachineState MoneyChangeRequerid = new MachineState(7, nameof(MoneyChangeRequerid).ToLowerInvariant());
        public static MachineState Cancelled = new MachineState(8, nameof(Cancelled).ToLowerInvariant());

        public MachineState(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<MachineState> List() =>
            new[] { MachineStarted,
                    ProductsLoaded,
                    CoinLoaded,
                    InsuficientMoneyLoaded,
                    SuficientMoneyLoaded,
                    ProductSelected,
                    MoneyChangeRequerid,
                    Cancelled
            };

        public static MachineState FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MachineStateException($"Possible values for MachineState: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MachineState From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MachineStateException($"Possible values for MachineState: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
