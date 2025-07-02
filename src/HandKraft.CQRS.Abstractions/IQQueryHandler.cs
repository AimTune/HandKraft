namespace HandKraft.Cqrs.Abstractions;

/// <summary>
/// Defines a handler for a query that returns an <see cref="IQueryable{T}"/>.
/// Typically used for OData or LINQ provider integration.
/// </summary>
/// <typeparam name="TQuery">The query type.</typeparam>
/// <typeparam name="T">The element type of the <see cref="IQueryable{T}"/> result.</typeparam>
public interface IQQueryHandler<TQuery, T>
    where TQuery : IQQuery<T>
{
    IQueryable<T> Handle(TQuery query);
}
