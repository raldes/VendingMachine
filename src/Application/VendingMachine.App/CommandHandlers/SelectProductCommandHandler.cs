using MediatR;
using VendingMachine.App.Commands;
using VendingMachine.App.Contracts;
using VendingMachine.App.Dtos;

namespace VendingMachine.App.CommandHandlers
{
    public class SelectProductCommandHandler : IRequestHandler<SelectProductCommand, DevolutionResponse>
    {
        private readonly IMachine _machine;

        public SelectProductCommandHandler(IMachine machine)
        {
            _machine = machine;
        }

        public Task<DevolutionResponse> Handle(SelectProductCommand request, CancellationToken cancellationToken)
        {
            var devolution = _machine.SellProduct(request.Product);

            var response = new DevolutionResponse
            {
                Devolution = devolution,
            };

            return Task.FromResult(response);
        }
    }
}
