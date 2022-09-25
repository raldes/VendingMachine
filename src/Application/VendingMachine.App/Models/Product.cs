using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.App.Models
{
    public record Product
    {
        public string Key { get; init; }
        public string Code { get; init; }
        public decimal Price { get; init; }
        public int Portions { get; set; }

        public Product(int key, string code, decimal price, int portions)
        {
            Key = key.ToString("00");
            Code = code;
            Price = price;
            Portions = portions;
        }
        
        public void RemovePortions(int removeQtty)
        {
            Portions -= removeQtty;
        }
    }
}
