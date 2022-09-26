using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Dtos;
using VendingMachine.App.Models;

namespace VendingMachine.App.Commands
{
    public class SelectProductCommand : IRequest<DevolutionResponse>
    {
        public Product Product { get; set; }
    }
}
