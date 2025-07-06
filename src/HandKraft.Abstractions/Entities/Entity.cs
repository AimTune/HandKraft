namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Entity base class that provides an identity and domain event support.
/// Entities are compared by their Id.
/// </summary>
/// <typeparam name="TKey">Type of the identifier.</typeparam>
public abstract class Entity<TKey>
{
    /// <summary>
    /// Unique identifier of the entity.
    /// </summary>
    public TKey Id { get; protected set; } = default!;

    private List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Read-only collection of domain events raised by this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="domainEvent">Domain event instance.</param>
    public void Announce(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes a domain event from the entity.
    /// </summary>
    /// <param name="domainEvent">Domain event instance.</param>
    public void Omit(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    public void Wipe()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Checks equality by comparing Id and type.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Entity<TKey>)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var other = (Entity<TKey>)obj;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    /// <summary>
    /// Returns hash code based on Id.
    /// </summary>
    public override int GetHashCode()
    {
        return EqualityComparer<TKey>.Default.GetHashCode(Id!);
    }
}