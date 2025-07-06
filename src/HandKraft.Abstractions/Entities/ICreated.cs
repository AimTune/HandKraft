namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Indicates the entity tracks creation audit information with a string user identifier.
/// This is a non-generic convenience interface inheriting from <see cref="ICreated{TUserId}"/> with <c>string</c> as the user ID type.
/// </summary>
public interface ICreated : ICreated<string> { }

/// <summary>
/// Indicates the entity tracks creation audit information with a generic user identifier.
/// </summary>
/// <typeparam name="TUserId">The type of the user identifier.</typeparam>
public interface ICreated<TUserId>
{
    /// <summary>
    /// Date and time when the entity was created.
    /// </summary>
    DateTimeOffset CreatedAt { get; }
    /// <summary>
    /// Identifier or name of the creator.
    /// </summary>
    TUserId CreatedBy { get; }
}