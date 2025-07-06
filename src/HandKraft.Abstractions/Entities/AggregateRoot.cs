namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Marker base class for aggregate roots.
/// Aggregates encapsulate domain entities and enforce consistency rules.
/// </summary>
/// <typeparam name="TKey">Type of the identifier.</typeparam>
public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    // You can extend this with aggregate-specific behaviors.
}