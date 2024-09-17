using MediatR;

namespace DeliveryApp.Application.Interfaces.Mediator;

public interface IQueryHandler<in TQuery, TResult>
    : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>{}
