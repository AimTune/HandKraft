namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Represents a domain event that describes something which happened in the domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the unique identifier of the domain event.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the date and time in UTC when the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}