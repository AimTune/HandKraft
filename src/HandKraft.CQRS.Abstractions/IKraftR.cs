using HandKraft.Abstractions.Results;

namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Mediator interface for sending commands and queries asynchronously.
/// Supports sending commands/queries and querying IQueryable results.
/// </summary>
public interface IKraftR
{
    /// <summary>
    /// Sends a command that returns a non-generic <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="TRequest">The command type.</typeparam>
    /// <param name="command">The command instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    Task<Result> Send<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result>;

    /// <summary>
    /// Sends a command that returns a <see cref="Result{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">The command type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="command">The command instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Result{TResponse}"/> with the command result.</returns>
    Task<Result<TResponse>> Send<TRequest, TResponse>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : IRequest<Result<TResponse>>;

    /// <summary>
    /// Sends a query that returns an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="TQuery">The IQueryable query type.</typeparam>
    /// <typeparam name="T">The element type of the IQueryable result.</typeparam>
    /// <param name="query">The query instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An <see cref="IQueryable{T}"/> representing the query result.</returns>
    IQueryable<T> Query<TQuery, T>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQQuery<T>;

    /// <summary>
    /// Sends a request and gets the typed response.
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    /// <param name="request">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The response.</returns>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a request dynamically (type erased).
    /// </summary>
    /// <param name="request">Request object.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The response as object, or null.</returns>
    Task<object?> Send(object request, CancellationToken cancellationToken = default);
}