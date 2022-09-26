using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.App.Dtos
{
    public record EnterCoinCommandResponse
    {
        public bool Result { get; init; }

        public EnterCoinCommandResponse(bool result)
        {
            Result = result;
        }
    }
}
