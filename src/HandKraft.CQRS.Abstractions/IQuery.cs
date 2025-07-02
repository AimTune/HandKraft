using HandKraft.Abstractions.Results;

namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Represents a query operation that returns a non-generic <see cref="Result"/> indicating success or failure.
/// Use for queries that do not return a value.
/// </summary>
//public interface IQuery : IRequest<Result> { }

/// <summary>
/// Represents a query operation that returns a <see cref="Result{T}"/>.
/// Use for queries that produce a value.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
