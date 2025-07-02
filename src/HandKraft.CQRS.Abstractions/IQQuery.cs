namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Represents a query that returns an <see cref="IQueryable{T}"/> sequence.
/// Typically used to integrate with OData or LINQ providers.
/// </summary>
/// <typeparam name="T">The element type of the queryable sequence.</typeparam>
public interface IQQuery<T> : IRequest<IQueryable<T>> { }