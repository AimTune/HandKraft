using HandKraft.Abstractions.Results;

namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Represents a command operation that returns a non-generic <see cref="Result"/> indicating success or failure.
/// Use for commands that do not return a value.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
/// Represents a command operation that returns a <see cref="Result{T}"/>.
/// Use for commands that produce a value.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;