using MediatR;

namespace DeliveryApp.Application.Interfaces.Mediator;

public interface IQuery<out T> : IRequest<T> { }
