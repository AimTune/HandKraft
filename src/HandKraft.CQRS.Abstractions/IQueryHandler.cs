using HandKraft.Abstractions.Results;

namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Defines a handler for a query that returns a <see cref="Result{T}"/>.
/// </summary>
/// <typeparam name="TQuery">The query type.</typeparam>
/// <typeparam name="TResponse">The type of the value contained in the result.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
