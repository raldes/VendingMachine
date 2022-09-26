using MediatR;
using VendingMachine.App.Commands;
using VendingMachine.App.Contracts;
using VendingMachine.App.Dtos;

namespace VendingMachine.App.CommandHandlers
{
    public class EnterCoinCommandHandler : IRequestHandler<EnterCoinCommand, CommandResponse>
    {
        private readonly IMachine _machine;

        public EnterCoinCommandHandler(IMachine machine)
        {
            _machine = machine;
        }

        Task<CommandResponse> IRequestHandler<EnterCoinCommand, CommandResponse>.Handle(EnterCoinCommand request, CancellationToken cancellationToken)
        {
            _machine.Wallet.AddCoinAmount(request.CoinAmount);
            return Task.FromResult(new CommandResponse { Result = true });
        }
    }
}
