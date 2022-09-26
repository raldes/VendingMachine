using MediatR;
using VendingMachine.App.Dtos;

namespace VendingMachine.App.Commands
{
    public class CancelCommand : IRequest<DevolutionResponse>
    {
        public bool Cancel { get; set; }
    }
}
