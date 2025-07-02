using HandKraft.Abstractions.Results;

namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Handles a command returning a non-generic Result.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Result> where TCommand : ICommand
{
}

/// <summary>
/// Handles a command returning a Result with a value.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
/// <typeparam name="TResponse">The type of the value returned in the result.</typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
{
}
