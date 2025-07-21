using HandKraft.Abstractions.Results;
using HandKraft.Cqrs.Abstractions;

namespace HandKraft.CQRS.KraftR;

public class KraftR : IKraftR
{
    private readonly IServiceProvider _serviceProvider;

    public KraftR(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<Result> Send<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result>
    {
        var handler = _serviceProvider.GetService(typeof(IRequestHandler<TRequest, Result>)) as IRequestHandler<TRequest, Result>;
        if (handler == null)
            throw new InvalidOperationException($"Handler not found for request type {typeof(TRequest).Name}");

        return handler.Handle(command, cancellationToken);
    }

    public Task<Result<TResponse>> Send<TRequest, TResponse>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result<TResponse>>
    {
        var handler = _serviceProvider.GetService(typeof(IRequestHandler<TRequest, Result<TResponse>>)) as IRequestHandler<TRequest, Result<TResponse>>;
        if (handler == null)
            throw new InvalidOperationException($"Handler not found for request type {typeof(TRequest).Name}");

        return handler.Handle(command, cancellationToken);
    }

    public IQueryable<T> Query<TQuery, T>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQQuery<T>
    {
        var handler = _serviceProvider.GetService(typeof(IQQueryHandler<TQuery, T>)) as IQQueryHandler<TQuery, T>;
        if (handler == null)
            throw new InvalidOperationException($"Handler not found for query type {typeof(TQuery).Name}");

        return handler.Handle(query);
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"Handler not found for request type {requestType.Name}");

        var methodInfo = handlerType.GetMethod("Handle");
        if (methodInfo == null)
            throw new InvalidOperationException($"Handle method not found on handler for {requestType.Name}");

        var task = methodInfo.Invoke(handler, new object[] { request, cancellationToken });
        return (Task<TResponse>)task!;
    }

    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        var requestInterface = requestType
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));

        if (requestInterface == null)
            throw new InvalidOperationException($"Request does not implement IRequest<T>: {requestType.Name}");

        var responseType = requestInterface.GetGenericArguments()[0];

        var sendMethod = typeof(KraftR)
            .GetMethods()
            .First(m => m.Name == nameof(Send)
                        && m.IsGenericMethodDefinition
                        && m.GetParameters().Length == 2
                        && m.GetParameters()[0].ParameterType == typeof(IRequest<>).MakeGenericType(m.GetGenericArguments()[0]));

        var genericSendMethod = sendMethod.MakeGenericMethod(responseType);

        var taskObj = genericSendMethod.Invoke(this, new object[] { request, cancellationToken });
        if (taskObj == null)
            return null;

        return await (dynamic)taskObj;
    }
}