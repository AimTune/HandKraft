namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Defines a handler for a request with a response.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request.
    /// </summary>
    /// <param name="request">The request instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A response.</returns>
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}