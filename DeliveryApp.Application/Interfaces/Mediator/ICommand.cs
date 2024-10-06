using MediatR;

namespace DeliveryApp.Application.Interfaces.Mediator;

public interface ICommand
{
    public interface ICommand<out TResult> : IRequest<TResult> { }
}
