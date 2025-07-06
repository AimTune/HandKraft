namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Base abstract class for domain events.
/// All domain events should inherit from this class.
///
/// Each event is uniquely identified by an <see cref="Id"/>
/// and records the UTC time it occurred in <see cref="OccurredOnUtc"/>.
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occurredOnUtc)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
    }

    /// <summary>
    /// Unique identifier of the domain event.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// UTC date and time when the event occurred.
    /// </summary>
    public DateTime OccurredOnUtc { get; init; }
}