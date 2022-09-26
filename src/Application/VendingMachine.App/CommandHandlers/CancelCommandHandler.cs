using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.App.Commands;
using VendingMachine.App.Contracts;
using VendingMachine.App.Dtos;
using VendingMachine.App.Models;

namespace VendingMachine.App.CommandHandlers
{
    public class CancelCommandHandler : IRequestHandler<CancelCommand, DevolutionResponse>
    {
        private readonly IMachine _machine;

        public CancelCommandHandler(IMachine machine)
        {
            _machine = machine;
        }
 
        Task<DevolutionResponse> IRequestHandler<CancelCommand, DevolutionResponse>.Handle(CancelCommand request, CancellationToken cancellationToken)
        {
            var devolution = _machine.CancelOperation();
            var response = new DevolutionResponse
            {
                Devolution = devolution,
            };
            return Task.FromResult(response);
        }
    }
}
