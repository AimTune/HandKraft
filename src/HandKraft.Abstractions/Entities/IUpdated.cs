namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Indicates the entity tracks update audit information with a string user identifier.
/// This is a non-generic convenience interface inheriting from <see cref="IUpdated{TUserId}"/> with <c>string</c> as the user ID type.
/// </summary>
public interface IUpdated : IUpdated<string> { }

/// <summary>
/// Indicates the entity tracks update audit information with a generic user identifier.
/// </summary>
/// <typeparam name="TUserId">The type of the user identifier.</typeparam>
public interface IUpdated<TUserId>
{
    /// <summary>
    /// Date and time when the entity was last updated.
    /// Nullable if never updated.
    /// </summary>
    DateTimeOffset UpdatedAt { get; }
    /// <summary>
    /// Identifier or name of the last updater.
    /// </summary>
    TUserId UpdatedBy { get; }
}