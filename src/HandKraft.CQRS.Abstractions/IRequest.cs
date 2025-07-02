namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Represents a request that returns a response of the specified type.
/// This is the base interface for commands and queries.
/// </summary>
public interface IRequest<TResult> { }