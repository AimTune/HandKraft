using HandKraft.Abstractions.Results;
using HandKraft.Cqrs.Abstractions;

namespace HandKraft.CQRS.KraftR;

public class KraftR(IServiceProvider serviceProvider) : IKraftR
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task<Result> Send<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result>
    {
        return _serviceProvider.GetService(typeof(IRequestHandler<TRequest, Result>)) is not IRequestHandler<TRequest, Result> handler
            ? throw new InvalidOperationException($"Handler not found {typeof(TRequest).Name}")
            : handler.Handle(command, cancellationToken);
    }

    public Task<Result<TResponse>> Send<TRequest, TResponse>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result<TResponse>>
    {
        return _serviceProvider.GetService(typeof(IRequestHandler<TRequest, Result<TResponse>>)) is not IRequestHandler<TRequest, Result<TResponse>> handler
            ? throw new InvalidOperationException($"Handler not found {typeof(TRequest).Name}")
            : handler.Handle(command, cancellationToken);
    }

    public IQueryable<T> Query<TQQuery, T>(TQQuery query, CancellationToken cancellationToken = default)
    where TQQuery : IQQuery<T>
    {
        return _serviceProvider.GetService(typeof(IQQueryHandler<TQQuery, T>)) is not IQQueryHandler<TQQuery, T> handler
            ? throw new InvalidOperationException($"Handler not found for {typeof(TQQuery).Name}")
            : handler.Handle(query);
    }
}