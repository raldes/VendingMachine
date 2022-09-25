using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.App.Models
{
    public record Coin
    {
        public string Code { get; init; }
        public decimal Value { get; init; }

        public Coin(string code, decimal value)
        {
            Code = code;
            Value = value;
        }
    }
}
